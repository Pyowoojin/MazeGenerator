using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
	private AudioSource audioSource;
	public AudioClip blockBrokenEffectSound;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = blockBrokenEffectSound;
	}

    private void Start()
    {
        GM_Script.StartListening(EventType.eBlockDeleted, SoundPlay);
    }

    void SoundPlay()
    {
        audioSource.Play();
    }
}