using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{

    public AudioManager audioManager;

    public float coolDown = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (coolDown <= 0)
        {

            if (collision.gameObject.layer == 9)
            {
                audioManager.PlayDamageClip();
                coolDown = 5;

                Debug.Log("Hit wall");
            }
        }
    }
    

    public void Update()
    {
        if(coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
        }
    }

}
