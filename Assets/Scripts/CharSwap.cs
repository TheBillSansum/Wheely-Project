using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSwap : MonoBehaviour
{

    public GameObject InvisChair;
    public GameObject MoveableChair;
    public GameObject BouncyChair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chair"))
        {
            InvisChair.SetActive(false);
            MoveableChair.SetActive(false);
            BouncyChair.SetActive(true);
        }
    }

}
