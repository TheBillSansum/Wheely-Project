using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageTracker : MonoBehaviour
{
    public AudioManager audioManager;

    public float DamageTaken = 0;
    public Image[] AmbientDamageColour;
    [Range(0f, 100f)] public float percentage = 0f;
    public TextMeshProUGUI PercentageText;

    public float coolDown = 0;
    public Slider CarImage;

    public void OnCollisionEnter(Collision collision)
    {
        if (coolDown <= 0)
        {
            if (collision.gameObject.layer == 9)
            {
                audioManager.PlayDamageClip();
                coolDown = 5;
                HitWall();
                Debug.Log("Hit wall");
            }
        }
    }

    public void Update()
    {
        CarImage.value = DamageTaken / 100f;

        if (coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
        }
    }

    public void HitWall()
    {
        DamageTaken += 10;
        percentage = DamageTaken;

        percentage = Mathf.Clamp(percentage, 0f, 100f);
        PercentageText.text = percentage.ToString() + "%";
        float alpha = percentage / 100f;

        Color newColor = new Color(1f, 0.2235f, 0.2235f, alpha);
        foreach (Image image in AmbientDamageColour)
        {
            image.color = newColor;
        }
    }
}