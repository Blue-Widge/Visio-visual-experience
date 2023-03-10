using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


/** @brief Class that handles the UI's button, in order to control the visual impairment and the scene */
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
    private string _tunnelVisionTextValue;
    private static GameObject _instance;
    public Toggle eyeTrackingToggle;
    public Toggle tunnelVisionToggle;
    public bool updatedThisScene = false;

    void Start()
    {
        DontDestroyOnLoad(this);
        if (_instance == null)
        {
            _instance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.activeSceneChanged += SceneChanged;
        eyeTracking = GameObject.Find("Tobii Gaze Visualizer").gameObject;
        tunnelVision = GameObject.Find("TunnelingVignette").gameObject;
        eyeTracking.SetActive(false);
        tunnelingVignetteController = tunnelVision.GetComponent<TunnelingVignetteController>();
        tunnelVisionSizeText.text = tunnelingVignetteController.defaultParameters.apertureSize.ToString();
        tunnelVisionSlider.value = tunnelingVignetteController.defaultParameters.apertureSize;
    }

    /** @brief Change the scene if updated */
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
        tunnelVision.transform.localRotation = Quaternion.identity;
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
        _tunnelVisionTextValue = Math.Round(tunnelVisionSlider.value, 3).ToString();
        tunnelVisionSizeText.text = _tunnelVisionTextValue;
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
                SceneManager.LoadScene("Kitchen contrast");
                break;
            case 1:
                SceneManager.LoadScene("Kitchen full");
                break;
            case 2:
                SceneManager.LoadScene("Kitchen white");
                break;
            default:
                break;
        }
    }

    /** @brief Sets the Eye tracking variable with the new scene components */
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
