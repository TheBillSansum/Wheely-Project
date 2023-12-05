using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenTyping : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public string message = "lsdkfjIOJASDIOjasmlsdlKSJDasuidhAKLSMalsmALKSJaihfKNASKnaskjHAUIDHFNJkjASHIHFIUhasdiuHASNKajshIUAHSaijndkjnglkASJOIasjALNFKJashiuhSDNASDLKJaushfakjnASUIHdjanbsfbIUANBDisjbfkajsbfIUSHadbfkjbASIDFbfAA";
    public float typingSpeed = 0.05f;

    private bool isWriting = false;
    private Coroutine typingCoroutine;

    private void Start()
    {
        textMeshPro.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWriting)
        {
            typingCoroutine = StartCoroutine(WriteText());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isWriting)
        {
            StopCoroutine(typingCoroutine);
            isWriting = false;
        }
    }

    IEnumerator WriteText()
    {
        isWriting = true;

        foreach (char letter in message.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isWriting = false;
    }
}
