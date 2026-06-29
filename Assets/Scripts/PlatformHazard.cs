using UnityEngine;

public class PlatformHazard : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.LoseLife();
        }
    }
}