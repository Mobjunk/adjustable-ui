using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScreenSize : MonoBehaviour
{
    [SerializeField] private Text screenSizeText;

    private void Awake()
    {
        screenSizeText = GetComponent<Text>();
    }

    void Update()
    {
        screenSizeText.text = $"{Screen.width} x {Screen.height}";
    }
}
