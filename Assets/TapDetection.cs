using UnityEngine;
using UnityEngine.EventSystems;

public class TapDetection : MonoBehaviour//, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector2 localTouchPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount <= 0) { 
            Debug.Log(" not Touch");
            return; }
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, null, out localPoint);

            
            //Debug.Log($"Local Touch Position: {localPoint}");
            //localTouchPosition = localPoint; // Store the local touch position
            bool isValid;
            int touchedLane = Utility.LocalPointToLane(localPoint, out isValid);
            if (!isValid) { 
                Debug.Log("Touch is outside");
                return; }
            //Debug.Log($"Lane: {touchedLane}");
            PlayManager.Instance.PlayerInput(i, touchedLane, touch);

        
            
        }
    }

 /*    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, Camera.main, out localPoint);
        localTouchPosition = localPoint; // Store the local touch position
        Debug.Log("Object Tapped!");
    }

    void OnTap()
    {
        //
        
    } */

    
}
