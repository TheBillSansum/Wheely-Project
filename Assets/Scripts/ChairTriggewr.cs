using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairTriggewr : MonoBehaviour
{
    public GameObject chairObject;
    public Rigidbody chairObjectRB;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chairObjectRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chairObjectRB.constraints = RigidbodyConstraints.None;
        }
    }
}
