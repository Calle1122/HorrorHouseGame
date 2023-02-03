using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TempCameraTriggerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera triggeredCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {
            TempControllerScript.currentActiveCam = triggeredCamera;
        }
    }
}
