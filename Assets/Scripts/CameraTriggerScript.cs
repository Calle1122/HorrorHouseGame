using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera triggerCamera;
    public string playerObjectTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            
            CameraManagerScript.currentActiveCamera = triggerCamera;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {

            CameraManagerScript.currentActiveCamera = triggerCamera;
        }
    }
    */
}
