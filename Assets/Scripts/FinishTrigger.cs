using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            PlayerControllerAR pc = other.GetComponent<PlayerControllerAR>();
            if (pc != null)
                pc.enabled = false;

            if (GameManager.Instance != null)
                GameManager.Instance.TriggerLevelComplete();
        }
    }
}