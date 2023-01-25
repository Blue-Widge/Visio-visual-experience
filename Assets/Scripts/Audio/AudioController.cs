using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that contains a method that plays a given audio */
public static class AudioController
{
    /** @brief Plays a given audio 
     \param[in] audioSource Audio to play
    \param[in] clip clip of the audio
    \param[in] loop boolean that says if it should loop or not
    */
    public static void PlayAudio(AudioSource audioSource, AudioClip audioClip, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
