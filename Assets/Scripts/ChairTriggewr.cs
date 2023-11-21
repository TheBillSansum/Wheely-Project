using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairTriggewr : MonoBehaviour
{
    public GameObject chairObject;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chairObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chairObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
