using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{
    AudioSource audioSource;
    bool isPlay = true;
    // ¿Ωæ«¿Ã OFF¿Œ∞°? 
    bool isOff = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GM_Script.StartListening(EventType.eGamePaused, MusicPause);
        GM_Script.StartListening(EventType.eBGMONOFF, MusicStop);
    }

    // Update is called once per frame
    void MusicPause()
    {
        if (!isOff)
        {
            if (isPlay)
            {
                audioSource.Pause();
                isPlay = false;
            }
            else if (!isPlay)
            {
                audioSource.UnPause();
                isPlay = true;
            }
        }
    }

    void MusicStop()
    {
        if (!isOff)
        {
            audioSource.Stop();
            isOff = true;
        }
        else if (isOff)
        {
            audioSource.Play();
            isOff = false;
        }

    }

}