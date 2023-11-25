using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] DamageClip;
    public AudioClip[] HintClip;
    public bool[] played = new bool[10];

    private AudioSource audioSource;
    private Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    public int hintClipCount = 0;
    private bool isPlaying = false;
    public GameObject skipHintObject;

    public TextMeshProUGUI textDisplay;
    public string[] lines;
    public float typingSpeed = 0.0005f;

    //public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineFreeLook cinemachineCamera;
    public Transform[] hintTransform;
    public Transform originalLookAt;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cinemachineCamera.LookAt = originalLookAt;
        PlayHint(0);
    }

    void Update()
    {
        if (!isPlaying && audioQueue.Count > 0)
        {
            StartCoroutine(PlayAudioFromQueue());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetTime();
        }
    }

    public void PlayDamageClip()
    {
        if (DamageClip.Length > 0)
        {
            int randomIndex = Random.Range(0, DamageClip.Length);
            AudioClip clip = DamageClip[randomIndex];
            EnqueueAudio(clip, false);
        }
    }

    public void PlayHint(int hintNumber)
    {
        if (!played[hintNumber])
        {
            AudioClip clip = HintClip[hintNumber];
            EnqueueAudio(clip, true);
            played[hintNumber] = true;
            TypeLine(hintNumber);

            if(hintTransform[hintNumber] != null)
            {
                cinemachineCamera.LookAt = hintTransform[hintNumber];
            }
        }
    }

    private void EnqueueAudio(AudioClip clip, bool isHint = false)
    {
        if (isHint)
        {
            StartCoroutine(PlayHintWithTimeScale(clip));
        }
        else
        {
           audioQueue.Enqueue(clip);
        }
    }

    private IEnumerator PlayAudioFromQueue()
    {
        if (audioQueue.Count > 0)
        {
            AudioClip clip = audioQueue.Dequeue();
            audioSource.PlayOneShot(clip);
            isPlaying = true;
            yield return new WaitForSeconds(clip.length / Time.deltaTime);
            isPlaying = false;
        }
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
        skipHintObject.SetActive(false);
        Debug.Log("Time set to 1");
        cinemachineCamera.LookAt = originalLookAt;
    }

    private IEnumerator PlayHintWithTimeScale(AudioClip clip)
    {
        skipHintObject.SetActive(true);
        hintClipCount++;
        Debug.Log("Hint started. Current hintClipCount: " + hintClipCount);

        try
        {
            if (Time.timeScale != 0.1f)
            {
                Time.timeScale = 0.1f;
            }

            audioSource.PlayOneShot(clip);
            isPlaying = true;
            yield return new WaitForSeconds(clip.length / 0.1f);
        }
        finally
        {
            hintClipCount--;
            Debug.Log("Hint ended. Current hintClipCount: " + hintClipCount);

            if (hintClipCount <= 0)
            {
                hintClipCount = 0; // Safeguard against negative values
                ResetTime();
            }

            isPlaying = false;
        }
    }

    public void TypeLine(int lineIndex)
    {
        if (lineIndex < 0 || lineIndex >= lines.Length)
        {
            Debug.LogError("Line index out of range.");
            return;
        }

        StopAllCoroutines(); // Stop any existing typing coroutine
        textDisplay.text = ""; // Clear existing text
        StartCoroutine(TypeOutLine(lines[lineIndex]));
    }

    IEnumerator TypeOutLine(string line)
    {
        foreach (char letter in line.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
