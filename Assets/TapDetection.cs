using UnityEngine;
using UnityEngine.EventSystems;

public class TapDetection : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector2 localTouchPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Touch detected");

                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, Camera.main, out localPoint);

                if (rectTransform.rect.Contains(localPoint))
                {
                    Debug.Log("Touch is within the bounds of the UI element");
                    localTouchPosition = localPoint; // Store the local touch position
                    OnTap();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, Camera.main, out localPoint);
        localTouchPosition = localPoint; // Store the local touch position
        OnTap();
    }

    void OnTap()
    {
        //Debug.Log("Object Tapped!");
        bool isValid;
        int laneIndex = Utility.CoorXToLane(localTouchPosition.x, out isValid);
        if (!isValid) { return; }
        Debug.Log($"Local Touch Position: {laneIndex}");
        // Extend functionality here
    }
}
