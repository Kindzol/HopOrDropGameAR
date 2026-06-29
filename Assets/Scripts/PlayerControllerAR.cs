using UnityEngine;

public class PlayerControllerAR : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 0.1f;
    public float maxSpeed = 2f;

    [Header("Jump")]
    public float jumpForce = 2.6f;
    public float jumpCooldown = 0.2f;

    [Header("Braking")]
    public float brakingForce = 5f;

    [Header("Ground detector")]
    public float groundCheckDistance = 0.03f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = true;
    private float jumpCooldownTimer = 0f;

    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        HandleJumpCooldown();

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began &&
                    touch.position.x > Screen.width * 0.5f)
                {
                    Jump();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        CheckGrounded();
        HandleMovement();
        ClampSpeed();
    }

    void HandleMovement()
    {
        if (moveInput != Vector2.zero)
        {
            Camera arCamera = Camera.main;
            Vector3 camForward = arCamera.transform.forward;
            Vector3 camRight = arCamera.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * moveInput.y + camRight * moveInput.x;
            rb.AddForce(moveDir * moveForce, ForceMode.Force);
        }
        else
        {
            Vector3 brakeForce = -rb.linearVelocity * brakingForce;
            brakeForce.y = 0f;
            rb.AddForce(brakeForce, ForceMode.Force);
        }
    }

    void HandleJumpCooldown()
    {
        if (!canJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
                canJump = true;
        }
    }

    void CheckGrounded()
    {
        float radius = 0.025f;
        Vector3 spherePos = transform.position + Vector3.down * 0.02f;
        isGrounded = Physics.CheckSphere(spherePos, radius, groundLayer);
    }

    void ClampSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 clamped = flatVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(clamped.x, rb.linearVelocity.y, clamped.z);
        }
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Jump()
    {
        Debug.Log("Jump called! isGrounded: " + isGrounded + " canJump: " + canJump);
        if (isGrounded && canJump)
        {
            Vector3 jumpDirection = Vector3.up + new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).normalized * 0.5f;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            canJump = false;
            jumpCooldownTimer = jumpCooldown;
        }
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}