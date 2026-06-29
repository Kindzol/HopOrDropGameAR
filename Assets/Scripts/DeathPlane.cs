using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private Vector3 spawnPoint;

    void Start()
    {
        spawnPoint = transform.parent.position + new Vector3(0f, 0.5f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlatformDisappearing[] platforms = FindObjectsByType<PlatformDisappearing>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var platform in platforms)
                platform.ResetPlatform();

            if (GameManager.Instance != null)
                GameManager.Instance.LoseLife();

            other.transform.position = spawnPoint;
            PlayerControllerAR pc = other.GetComponent<PlayerControllerAR>();
            if (pc != null)
                pc.ResetVelocity();
        }
    }
}