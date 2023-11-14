using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippedCheck : MonoBehaviour
{
   public AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            audioManager.PlayHint(4);
        }
    }
}
