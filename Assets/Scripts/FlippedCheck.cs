using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippedCheck : MonoBehaviour
{
    public AudioManager audioManager;
    private Coroutine waitCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && waitCoroutine == null)
        {
            waitCoroutine = StartCoroutine(PlaySoundAfterDelay(3));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") && waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
    }

    private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioManager.PlayHint(4);
        waitCoroutine = null;
    }
}