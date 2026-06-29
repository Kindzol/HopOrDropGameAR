using UnityEngine;

public class PlatformDisappearing : MonoBehaviour
{
    public float disappearDelay = 0.5f;
    private bool triggered = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            Invoke(nameof(Disappear), disappearDelay);
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    public void ResetPlatform()
    {
        triggered = false;
        gameObject.SetActive(true);
    }
}