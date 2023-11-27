using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKnockOff : MonoBehaviour
{
    public int[] CarNumber = new int[4];
    public AudioManager audioManager;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Car1":
                CarNumber[0] = 1;
                CarHit();
                break;

            case "Car2":
                CarNumber[1] = 1;
                CarHit();
                break;

            case "Car3":
                CarNumber[2] = 1;
                CarHit();
                break;

            case "Car4":
                CarNumber[3] = 1;
                CarHit();
                break;

            case "Car5":
                CarNumber[4] = 1;
                CarHit();
                break;
        }
    }

    public void CarHit()
    {
        audioManager.PlayHint(11);
    }
}
