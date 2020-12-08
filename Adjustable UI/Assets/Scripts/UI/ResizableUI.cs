using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ResizableUI : MonoBehaviour
{
    RectTransform parent;
    [SerializeField] Type type;
    EventTrigger eventTrigger;
    Vector2 minimumDimmensions;
    [SerializeField] Vector2 maximumDimmensions;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<RectTransform>();

        minimumDimmensions = parent.sizeDelta;

        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
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
                float inset = Screen.width - parent.position.x - parent.pivot.x * parent.rect.width;
                float size = Mathf.Clamp(parent.rect.width - ped.delta.x, minimumDimmensions.x, maximumDimmensions.x);
                parent.SetInsetAndSizeFromParentEdge((RectTransform.Edge)horizontalEdge, inset, size);
            }
            else
            {
                float inset = parent.position.x - parent.pivot.x * parent.rect.width;
                float size = Mathf.Clamp(parent.rect.width + ped.delta.x, minimumDimmensions.x, maximumDimmensions.x);
                parent.SetInsetAndSizeFromParentEdge((RectTransform.Edge)horizontalEdge, inset, size);

            }
        }
        if (verticalEdge != null)
        {
            if (verticalEdge == RectTransform.Edge.Top)
            {
                float inset = Screen.height - parent.position.y - parent.pivot.y * parent.rect.height;
                float size = Mathf.Clamp(parent.rect.height - ped.delta.y, minimumDimmensions.y, maximumDimmensions.y);
                parent.SetInsetAndSizeFromParentEdge((RectTransform.Edge)verticalEdge, inset, size);
            }
            else
            {
                float inset = parent.position.y - parent.pivot.y * parent.rect.height;
                float size = Mathf.Clamp(parent.rect.height + ped.delta.y, minimumDimmensions.y, maximumDimmensions.y);
                parent.SetInsetAndSizeFromParentEdge((RectTransform.Edge)verticalEdge, inset, size);
            }
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
}
