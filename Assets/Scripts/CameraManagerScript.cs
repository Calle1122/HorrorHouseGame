using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera camera1 = null;
    [SerializeField] CinemachineVirtualCamera camera2 = null;
    [SerializeField] CinemachineVirtualCamera camera3 = null;
    [SerializeField] CinemachineVirtualCamera camera4 = null;
    [SerializeField] CinemachineVirtualCamera camera5 = null;
    [SerializeField] CinemachineVirtualCamera camera6 = null;
    [SerializeField] CinemachineVirtualCamera camera7 = null;
    [SerializeField] CinemachineVirtualCamera camera8 = null;
    [SerializeField] CinemachineVirtualCamera camera9 = null;
    [SerializeField] CinemachineVirtualCamera camera10 = null;
    private List<CinemachineVirtualCamera> allMyCameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera currentActiveCamera = null;

    private void Awake()
    {
        PopulateCameraList();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentActiveCamera = startCamera;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SwitchToCamera(currentActiveCamera);
        //Debug.Log(currentActiveCamera);
    }

    private void PopulateCameraList()
    {
        if (camera1 != null)
        {
            allMyCameras.Add(camera1);
        }

        if (camera2 != null)
        {
            allMyCameras.Add(camera2);
        }

        if (camera3 != null)
        {
            allMyCameras.Add(camera3);
        }

        if (camera4 != null)
        {
            allMyCameras.Add(camera4);
        }

        if (camera5 != null)
        {
            allMyCameras.Add(camera5);
        }

        if (camera6 != null)
        {
            allMyCameras.Add(camera6);
        }

        if (camera7 != null)
        {
            allMyCameras.Add(camera7);
        }

        if (camera8 != null)
        {
            allMyCameras.Add(camera8);
        }

        if (camera9 != null)
        {
            allMyCameras.Add(camera9);
        }

        if (camera10 != null)
        {
            allMyCameras.Add(camera10);
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera newActiveCam)
    {
        
        if (newActiveCam.Priority != 10)
        {
            
            //Debug.Log(currentActiveCamera);
            foreach(CinemachineVirtualCamera cammy in allMyCameras)
            {
                cammy.Priority = 0;
            }
            newActiveCam.Priority = 10;
            
        }
    }

    


}
