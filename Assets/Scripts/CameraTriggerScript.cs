using System.Collections.Generic;
using Cinemachine;
using GameConstants;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera triggerCamera;

    [SerializeField] private bool isMainRoomTrigger;
    [SerializeField] private List<CameraTriggerScript> roomsToReset;

    [HideInInspector]public bool hasDoneMainTransition;
    
    private bool _ghostEntered, _humanEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerTag))
        {
            _humanEntered = true;
        }

        if (other.CompareTag(Tags.GhostTag))
        {
            _ghostEntered = true;
        }

        if (_humanEntered && _ghostEntered)
        {
            SetActiveCamera();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMainRoomTrigger && !hasDoneMainTransition)
        {
            return;
        }
        
        if (other.CompareTag(Tags.PlayerTag))
        {
            _humanEntered = false;
        }

        if (other.CompareTag(Tags.GhostTag))
        {
            _ghostEntered = false;
        }
    }

    private void SetActiveCamera()
    {
        CameraManagerScript.CurrentActiveCamera = triggerCamera;
        _ghostEntered = false;
        _humanEntered = false;

        if (!isMainRoomTrigger || hasDoneMainTransition)
        {
            return;
        }

        hasDoneMainTransition = true;
        
        foreach (CameraTriggerScript camTrigger in roomsToReset)
        {
            camTrigger.hasDoneMainTransition = false;
        }
    }
}