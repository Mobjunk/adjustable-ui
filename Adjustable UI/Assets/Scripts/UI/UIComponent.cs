using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] Alignment currentAlignment;
    
    public Vector2 minimumDimmensions;
    public Vector2 maximumDimmensions;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        minimumDimmensions = rectTransform.sizeDelta;
    }

    private enum Alignment
    {
        CENTER,
        UPPER_LEFT,
        UPPER_RIGHT,
        LOWER_LEFT,
        LOWER_RIGHT,
        UPPER_MIDDLE,
        LOWER_MIDDLE,
        LEFT,
        RIGHT
    }
}
