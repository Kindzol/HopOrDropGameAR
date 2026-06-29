using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Joystick Settings")]
    public RectTransform background;
    public RectTransform handle;
    public float handleRange = 50f;

    private Vector2 input = Vector2.zero;
    private PlayerControllerAR playerController;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.2f;

    public void SetPlayerController(PlayerControllerAR pc)
    {
        playerController = pc;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (Time.time - lastTapTime < doubleTapThreshold)
        {
            if (playerController != null)
                playerController.Jump();
        }
        lastTapTime = Time.time;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, eventData.pressEventCamera, out pos);

        pos = Vector2.ClampMagnitude(pos, handleRange);
        handle.localPosition = pos;

        input = pos / handleRange;
        if (playerController != null)
            playerController.SetMoveInput(input);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.localPosition = Vector2.zero;
        if (playerController != null)
            playerController.SetMoveInput(Vector2.zero);
    }
}