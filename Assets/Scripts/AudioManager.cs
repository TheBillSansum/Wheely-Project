using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] DamageClip;
    public AudioClip[] HintClip;
    public bool[] played = new bool[10];


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();       
        PlayHint(0);
    }


    public void PlayDamageClip()
    {
        if (DamageClip.Length > 0)
        {
            int randomIndex = Random.Range(0, DamageClip.Length);
            AudioClip clip = DamageClip[randomIndex];
            PlayAudio(clip);
        }
    }


    public void PlayHint(int hintNumber)
    {
        if (played[hintNumber] == false)
        {
            AudioClip clip = HintClip[hintNumber];
            PlayAudio(clip);
            played[hintNumber] = true;
        }
    }

    public void PlayAudio(AudioClip clip)
    {
            audioSource.PlayOneShot(clip);
    }
}
