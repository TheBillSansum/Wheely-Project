using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMugTrigger : MonoBehaviour
{
    public AudioManager audioManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CoffeeMug")
        {
            audioManager.PlayHint(10);
        }

    }
}
