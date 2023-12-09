using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceInstance : MonoBehaviour
{
    public bool destroyed = false;
    public int evidenceNumber;
    public EvidenceManager evidenceManager;
    public GameObject hintHighlight;


    public void Start()
    {
        hintHighlight.SetActive(false);
       // Invoke("EnableHintHighlight", 3);
    }

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
        hintHighlight.SetActive(false);
    }

    public void EnableHintHighlight()
    {
        hintHighlight.SetActive(true);
        StartCoroutine(PulseHintHighlight());
    }

    private IEnumerator PulseHintHighlight()
    {
        float duration = 10f;
        float magnitude = 0.1f;
        Vector3 originalScale = hintHighlight.transform.localScale;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float scale = Mathf.PingPong(elapsed / duration * 2f, 1f) * magnitude + 1f;
            hintHighlight.transform.localScale = originalScale * scale;

            elapsed += Time.deltaTime;
            yield return null;
        }

        hintHighlight.transform.localScale = originalScale;
    }
}
