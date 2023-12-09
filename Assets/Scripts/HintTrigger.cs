using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{

    public int hintNumber;
    public AudioManager audioManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlayHint(hintNumber);
            Debug.Log(other.gameObject.name);
        }

        if (other.CompareTag("Domino"))
        {
            audioManager.PlayHint(hintNumber);
            Time.timeScale = 1;
        }
    }
}
