using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScaler : MonoBehaviour
{
    private CanvasScaler _canvasScaler;
    
    void Start()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        _canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }
}
