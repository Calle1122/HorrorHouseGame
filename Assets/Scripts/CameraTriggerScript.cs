using Cinemachine;
using GameConstants;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera triggerCamera;

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
    }
}