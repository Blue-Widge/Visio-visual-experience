using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UIButtons : MonoBehaviour
{
    public GameObject eyeTracking;
    public GameObject tunnelVision;
    public TunnelingVignetteController tunnelingVignetteController;
    public Slider tunnelVisionSlider;
    public TextMeshProUGUI tunnelVisionSizeText;
    private string tunnelVisionTextValue;

    // Start is called before the first frame update
    void Start()
    {
        tunnelVisionSizeText.text = tunnelingVignetteController.defaultParameters.apertureSize.ToString();
        tunnelVisionSlider.value = tunnelingVignetteController.defaultParameters.apertureSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEyeTracking()
    {
        eyeTracking.SetActive(!eyeTracking.activeInHierarchy);
    }

    public void ToggleTunnelVision()
    {
        tunnelVision.SetActive(!tunnelVision.activeInHierarchy);
    }

    public void TunnelVisionSize()
    {
        tunnelingVignetteController.defaultParameters.apertureSize = tunnelVisionSlider.value;
        tunnelVisionTextValue = Math.Round(tunnelVisionSlider.value, 3).ToString();
        tunnelVisionSizeText.text = tunnelVisionTextValue;
    }
}
