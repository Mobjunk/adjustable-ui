using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableUI : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    /// <summary>
    /// The size of the screen the player is on
    /// </summary>
    [SerializeField] Rect screenRect;
    /// <summary>
    /// The parent of the title
    /// </summary>
    [SerializeField] RectTransform parent;
    /// <summary>
    /// The canvas this UI is linked in
    /// </summary>
    [SerializeField] Canvas canvas;
    /// <summary>
    /// The last valid position within the screen for the UI element
    /// </summary>
    [SerializeField] Vector2 lastValidPosition;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<RectTransform>();
        canvas = parent.transform.parent.gameObject.GetComponent<Canvas>();
        screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    //TODO: Make it always go back to the mouse position when its within the screen again and stuff
    public void OnDrag(PointerEventData eventData)
    {
        //Handles dragging the UI element around the screen
        Vector2 position = eventData.delta / canvas.scaleFactor;
        parent.anchoredPosition += position;

        //Handles converting all the corerns of the UI element to a array of vector3's
        Vector3[] objectCorners = new Vector3[4];
        parent.GetWorldCorners(objectCorners);

        //Handles checking all the corners of the UI element
        bool isOverflowing = false;
        foreach (Vector3 corner in objectCorners)
            if (!screenRect.Contains(corner))
            {
                isOverflowing = true;
                break;
            }

        //Handles checking if the UI is going out the screen
        if (isOverflowing)
        {
            parent.anchoredPosition = lastValidPosition;
            return;
        }

        //Handles setting the last valid position of the this UI element
        lastValidPosition = parent.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Makes it so the UI element your dragging is always on top
        parent.SetAsLastSibling();
    }
}
