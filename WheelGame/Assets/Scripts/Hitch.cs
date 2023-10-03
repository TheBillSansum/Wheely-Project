using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hitch : MonoBehaviour
{

    public ConfigurableJoint hitchJoint;
    public bool connected;
    public bool connectable;
    public GameObject closeObject;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && connected)
        {
            hitchJoint.connectedBody = null;
            connected = false;
            closeObject = null;
        }


        if (Input.GetKeyDown(KeyCode.Space) && connectable)
        {
            hitchJoint.connectedBody = closeObject.GetComponent<Rigidbody>();
            connected = true;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>() && other.CompareTag("Hitchable"))
        {
            connectable = true;
            closeObject = other.gameObject;
        }
        else
        {
            connectable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() && other.CompareTag("Hitchable"))
        {
            connectable = false;
        }
    }

    
}
