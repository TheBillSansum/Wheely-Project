using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public EvidenceInstance[] evidenceObject;
    public GameObject UIEvidenceParent;
    public GameObject[] UIEvidenceScribble;


    public void Start()
    {
        UIEvidenceParent.SetActive(false);
    }

    public void EvidenceDestroyed(int evidenceNumber)
    {
        UIEvidenceParent.SetActive(true);
        UIEvidenceScribble[evidenceNumber].SetActive(true);
    }

}
