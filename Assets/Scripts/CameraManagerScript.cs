using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public static CinemachineVirtualCamera CurrentActiveCamera;
    [SerializeField] private CinemachineVirtualCamera startCamera;
    [SerializeField] private Camera uICam;
    [SerializeField] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    private void Start()
    {
        CurrentActiveCamera = startCamera;
        uICam.fieldOfView = startCamera.m_Lens.FieldOfView;
    }

    private void LateUpdate()
    {
        SwitchToCamera();
    }

    private void SwitchToCamera()
    {
        if (CurrentActiveCamera.Priority == 10)
        {
            return;
        }

        foreach (var virtualCamera in cameras)
        {
            virtualCamera.Priority = 0;
        }

        CurrentActiveCamera.Priority = 10;
        uICam.fieldOfView = CurrentActiveCamera.m_Lens.FieldOfView;
    }
}