using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public int currentCameraPos = 1;
    public GameObject virtualCamera;
    public GameObject mainCamera;
    public GameObject[] cameraTransform;


    void Start()
    {
        UpdateCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentCameraPos = 0;
            UpdateCamera();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            currentCameraPos = 1;
            UpdateCamera();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentCameraPos = 1;
            UpdateCamera();
        }
    }

    public void UpdateCamera()
    {
    switch (currentCameraPos)
        {
            case 0:
                virtualCamera.SetActive(true);
                mainCamera.transform.position = cameraTransform[0].transform.position;
                    break;

            case 1:
                virtualCamera.SetActive(false);
                mainCamera.transform.position = cameraTransform[1].transform.position;
                mainCamera.transform.rotation = Quaternion.Euler(30, 0, 0);
                break;

            case 2:
                virtualCamera.SetActive(false);
                mainCamera.transform.position = cameraTransform[2].transform.position;
                mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;


        }    
    }
}
