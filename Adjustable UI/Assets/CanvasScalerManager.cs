using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerManager : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }
}
