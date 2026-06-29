using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 10f;
    public float maxSpeed = 8f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float jumpCooldown = 0.2f;

    [Header("Braking")]
    public float brakingForce = 5f;

    [Header("Ground detector")]
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private bool canJump = true;
    private float jumpCooldownTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        HandleJumpCooldown();
        HandleJump();
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
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) vertical = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;

        Vector3 force = new Vector3(horizontal, 0f, vertical) * moveForce;
        rb.AddForce(force, ForceMode.Force);

        
        if (vertical == 0f && horizontal == 0f)
        {
            Vector3 brakeForce = -rb.linearVelocity * brakingForce;
            brakeForce.y = 0f;
            rb.AddForce(brakeForce, ForceMode.Force);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            canJump = false;
            jumpCooldownTimer = jumpCooldown;
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
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );
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

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}