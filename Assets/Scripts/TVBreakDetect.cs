using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBreakDetect : MonoBehaviour
{

    public bool tvBroke = false;
    public GameObject tvScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (tvBroke == false)
        {
            tvBroke = true;
            tvScreen.gameObject.SetActive(true);
        }
    }


}
