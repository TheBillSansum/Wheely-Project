using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceInstance : MonoBehaviour
{
    public bool destroyed = false;
    public int evidenceNumber;
    public EvidenceManager evidenceManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && destroyed == false)
        {
            PlayerDestoryed();
        }
    } 

    public void PlayerDestoryed()
    {
        destroyed = true;
        evidenceManager.EvidenceDestroyed(evidenceNumber);
    }


}
