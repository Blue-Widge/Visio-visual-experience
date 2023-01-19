using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFlickerButton : MonoBehaviour
{
    public Material flickMat;
    public AudioSource phoneTringAudioSource;
    public bool phoneIsTringing;
    public float timer = 0;
    public float timerCoolDown;



    // Start is called before the first frame update
    void Start()
    {
       // flickMat = GetComponent<Material>();
    }

    // Update is called once per frame
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