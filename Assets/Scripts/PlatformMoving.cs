using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        pointA = transform.position;
        pointB = transform.position + new Vector3(4f, 0f, 0f);
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}