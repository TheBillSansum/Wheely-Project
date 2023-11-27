
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public int boost = 100;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarTest>() != null)
            other.gameObject.GetComponent<Rigidbody>().AddForce(boost, 0, 0);
            Debug.Log(other);
        } 
    }

