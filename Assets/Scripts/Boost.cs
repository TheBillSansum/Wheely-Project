using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(100, 0, 0);
        Debug.Log(other);
    }
}
