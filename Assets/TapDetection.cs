using UnityEngine;
//using UnityEngine.EventSystems;

public class TapDetection : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount <= 0) 
        { 
            //Debug.Log(" not Touch");
            return; 
        }
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, touch.position, null, out localPoint);
            //Debug.Log($"Local Touch Position: {localPoint}");

            PlayManager.Instance.TouchToGameInput(i, localPoint, touch);
        }
    }
    
}
