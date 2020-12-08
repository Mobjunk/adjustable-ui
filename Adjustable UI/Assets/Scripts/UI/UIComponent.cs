using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    [SerializeField] Alignment currentAlignment;

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
