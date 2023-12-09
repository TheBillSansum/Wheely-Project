using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHint : MonoBehaviour
{
    public EvidenceManager evidenceManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DisplayHints", 300);
        }
    }

    public void DisplayHints()
    {
        evidenceManager.DisplayHint();
    }
}
