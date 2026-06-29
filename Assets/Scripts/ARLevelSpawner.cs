using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARLevelSpawner : MonoBehaviour
{
    [Header("AR Components")]
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    [Header("Level")]
    public GameObject levelPrefab;

    [Header("UI")]
    public GameplayUI gameplayUI;
    public VirtualJoystick joystick;

   
    private GameObject spawnedLevel;
    
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool levelPlaced = false;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (levelPlaced) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing &&
            GameManager.Instance.CurrentState != GameManager.GameState.MainMenu) return;

        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];
            Debug.Log("Touch detected: " + touch.phase);

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Vector2 touchPosition = touch.screenPosition;
                bool hit = raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
                Debug.Log("Raycast hit: " + hit);

                if (hit)
                {
                    Pose hitPose = hits[0].pose;
                    spawnedLevel = Instantiate(levelPrefab, hitPose.position, hitPose.rotation);
                    levelPlaced = true;

                    PlayerControllerAR player = spawnedLevel.GetComponentInChildren<PlayerControllerAR>();
                    if (player != null && gameplayUI != null)
                        gameplayUI.playerController = player;

                    if (joystick != null)
                        joystick.SetPlayerController(player);

                    

                    planeManager.enabled = false;
                    foreach (var plane in planeManager.trackables)
                        plane.gameObject.SetActive(false);

                    if (GameManager.Instance != null)
                        GameManager.Instance.SetState(GameManager.GameState.Playing);
                }
            }
        }
    }

    public void ResetLevel()
    {
        if (spawnedLevel != null)
            Destroy(spawnedLevel);

        levelPlaced = false;
        planeManager.enabled = true;
        planeManager.gameObject.SetActive(true);
    }
}