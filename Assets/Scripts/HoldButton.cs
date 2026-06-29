using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private bool isHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        onPress.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        onRelease.Invoke();
    }

    void OnDisable()
    {
        if (isHeld)
        {
            isHeld = false;
            onRelease.Invoke();
        }
    }
}