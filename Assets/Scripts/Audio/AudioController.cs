using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{
   public static void PlayAudio(AudioSource audioSource, AudioClip audioClip, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
