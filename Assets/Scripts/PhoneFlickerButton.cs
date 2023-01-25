using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that handles the keypad LEDs on the phone */

public class PhoneFlickerButton : MonoBehaviour
{
    /** @brief Material of the LED */
    public Material flickMat;
    public AudioSource phoneTringAudioSource;
    public bool phoneIsTringing;
    public float timer = 0;
    public float timerCoolDown;

    /** @brief Sets the LED to black if the phone isn't ringing, otherwise makes it flick between black and red */
    void Update()
    {
        phoneIsTringing = phoneTringAudioSource.isPlaying;
        if (!phoneIsTringing)
        {
            flickMat.SetColor("_EmissionColor", Color.black);
            return;
        }
        timer += Time.deltaTime;
        if (timer > timerCoolDown)
        {
            timer = 0;
            if (flickMat.GetColor("_EmissionColor") == Color.black)
            flickMat.SetColor("_EmissionColor", Color.red);
            else
            flickMat.SetColor("_EmissionColor", Color.black);
        }
    }
}