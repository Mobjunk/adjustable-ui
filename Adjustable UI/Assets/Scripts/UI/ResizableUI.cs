using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ResizableUI : MonoBehaviour
{
    [SerializeField] Rect screenRect;
    [SerializeField] RectTransform parent;
    [SerializeField] Type type;
    EventTrigger eventTrigger;
    Vector2 minimumDimmensions;
    [SerializeField] Vector2 maximumDimmensions;

    private SizeData sizeData;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<RectTransform>();

        minimumDimmensions = parent.sizeDelta;

        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
        
        screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        
        sizeData = new SizeData(parent.pivot, parent.anchorMin, parent.anchorMax, parent.anchoredPosition, parent.sizeDelta);
    }

    private void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;
        RectTransform.Edge? horizontalEdge = null;
        RectTransform.Edge? verticalEdge = null;

        switch (type)
        {
            case Type.TopRight:
                horizontalEdge = RectTransform.Edge.Left;
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            case Type.Right:
                horizontalEdge = RectTransform.Edge.Left;
                break;
            case Type.BottomRight:
                horizontalEdge = RectTransform.Edge.Left;
                verticalEdge = RectTransform.Edge.Top;
                break;
            case Type.Bottom:
                verticalEdge = RectTransform.Edge.Top;
                break;
            case Type.BottomLeft:
                horizontalEdge = RectTransform.Edge.Right;
                verticalEdge = RectTransform.Edge.Top;
                break;
            case Type.Left:
                horizontalEdge = RectTransform.Edge.Right;
                break;
            case Type.TopLeft:
                horizontalEdge = RectTransform.Edge.Right;
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            case Type.Top:
                verticalEdge = RectTransform.Edge.Bottom;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //TODO: Add a way to make the ui element not go outside the screen
        if (horizontalEdge != null)
        {
            if (horizontalEdge == RectTransform.Edge.Right)
            {
                float inset = screenRect.width - parent.position.x - parent.pivot.x * parent.rect.width;
                float size = Mathf.Clamp(parent.rect.width - ped.delta.x, minimumDimmensions.x, maximumDimmensions.x);
                ResizeFrame((RectTransform.Edge)horizontalEdge, inset, size);
            }
            else
            {
                float inset = parent.position.x - parent.pivot.x * parent.rect.width;
                float size = Mathf.Clamp(parent.rect.width + ped.delta.x, minimumDimmensions.x, maximumDimmensions.x);
                ResizeFrame((RectTransform.Edge)horizontalEdge, inset, size);

            }
        }
        if (verticalEdge != null)
        {
            if (verticalEdge == RectTransform.Edge.Top)
            {
                float inset = screenRect.height - parent.position.y - parent.pivot.y * parent.rect.height;
                float size = Mathf.Clamp(parent.rect.height - ped.delta.y, minimumDimmensions.y, maximumDimmensions.y);
                ResizeFrame((RectTransform.Edge)verticalEdge, inset, size);
            }
            else
            {
                float inset = parent.position.y - parent.pivot.y * parent.rect.height;
                float size = Mathf.Clamp(parent.rect.height + ped.delta.y, minimumDimmensions.y, maximumDimmensions.y);
                ResizeFrame((RectTransform.Edge)verticalEdge, inset, size);
            }
        }
    }

    public void ResizeFrame(RectTransform.Edge edge, float inset, float size)
    {
        parent.SetInsetAndSizeFromParentEdge(edge, inset, size);

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
        
        //Handles setting the last valid position of the this UI element
        if (!isOverflowing)
        {
            sizeData.AnchorMin = parent.anchorMin;
            sizeData.AnchorMax = parent.anchorMax;
            sizeData.AnchoredPosition = parent.anchoredPosition;
            sizeData.SizeDelta = parent.sizeDelta;
        }
        else
        {
            parent.anchorMin = sizeData.AnchorMin;
            parent.anchorMax = sizeData.AnchorMax;
            parent.anchoredPosition = sizeData.AnchoredPosition;
            parent.sizeDelta = sizeData.SizeDelta;
        }
    }
    
    public enum Type
    {
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft,
        Top
    }
    
    [Serializable]
    public class SizeData
    {
        private Vector2 pivot;

        public Vector2 Pivot
        {
            get => pivot;
            set => pivot = value;
        }

        private Vector2 anchorMin;

        public Vector2 AnchorMin
        {
            get => anchorMin;
            set => anchorMin = value;
        }

        private Vector2 anchorMax;

        public Vector2 AnchorMax
        {
            get => anchorMax;
            set => anchorMax = value;
        }

        private Vector2 anchoredPosition;

        public Vector2 AnchoredPosition
        {
            get => anchoredPosition;
            set => anchoredPosition = value;
        }

        private Vector2 sizeDelta;

        public SizeData(Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 sizeDelta)
        {
            this.pivot = pivot;
            this.anchorMin = anchorMin;
            this.anchorMax = anchorMax;
            this.anchoredPosition = anchoredPosition;
            this.sizeDelta = sizeDelta;
        }

        public Vector2 SizeDelta
        {
            get => sizeDelta;
            set => sizeDelta = value;
        }
    }
}
