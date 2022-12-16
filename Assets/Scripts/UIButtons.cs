using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject eyeTracking;
    [SerializeField]
    private GameObject tunnelVision;
    [SerializeField]
    private TunnelingVignetteController tunnelingVignetteController;
    public Slider tunnelVisionSlider;
    public TextMeshProUGUI tunnelVisionSizeText;
    private string tunnelVisionTextValue;
    private static GameObject instance;
    public Toggle eyeTrackingToggle;
    public Toggle tunnelVisionToggle;
    public bool updatedThisScene = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.activeSceneChanged += SceneChanged;
        eyeTracking = GameObject.Find("Tobii Gaze Visualizer").gameObject;
        tunnelVision = GameObject.Find("TunnelingVignette").gameObject;
        tunnelingVignetteController = tunnelVision.GetComponent<TunnelingVignetteController>();
        tunnelVisionSizeText.text = tunnelingVignetteController.defaultParameters.apertureSize.ToString();
        tunnelVisionSlider.value = tunnelingVignetteController.defaultParameters.apertureSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (updatedThisScene)
        {
            TunnelVisionSize();
            updatedThisScene = false;
        }
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
        Debug.LogWarning("slider size= " + tunnelVisionSlider.value);
        Debug.LogWarning("size= " + tunnelingVignetteController.defaultParameters.apertureSize);
        tunnelVisionTextValue = Math.Round(tunnelVisionSlider.value, 3).ToString();
        tunnelVisionSizeText.text = tunnelVisionTextValue;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchKitchen(int number) 
    {
        switch (number) 
        {
            case 0:
                SceneManager.LoadScene("Kitchen");
                break;
            case 1:
                SceneManager.LoadScene("Kitchen SameColours");
                break;
            case 2:
                SceneManager.LoadScene("Kitchen RandomColor");
                break;
            default:
                break;
        }
    }

    void SceneChanged(Scene current, Scene next)
    {
        eyeTracking = GameObject.Find("Tobii Gaze Visualizer").gameObject;
        tunnelVision = GameObject.Find("TunnelingVignette").gameObject;
        tunnelingVignetteController = tunnelVision.GetComponent<TunnelingVignetteController>();
        eyeTracking.SetActive(eyeTrackingToggle.isOn);
        tunnelVision.SetActive(tunnelVisionToggle.isOn);
        updatedThisScene = true;
    }
}
