using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipBackground;
    public AudioClip aoudioClipEffect;

    public static SoundManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    void PlayBackground()
    {
        audioSource.PlayOneShot(audioClipBackground);
    }

    void PlayEffect()
    {
        audioSource.PlayOneShot(aoudioClipEffect);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
