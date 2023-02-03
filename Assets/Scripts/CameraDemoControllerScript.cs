using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.InputSystem;

public class CameraDemoControllerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineVirtualCamera camera3;
    [SerializeField] private CinemachineVirtualCamera camera4;
    [SerializeField] private CinemachineVirtualCamera camera5;
    List<CinemachineVirtualCamera> myCamList = new List<CinemachineVirtualCamera>();

    [SerializeField] private TMP_Text cameraNameText;

    private Keyboard myKB;

    private void Awake()
    {
        myKB = Keyboard.current;
    }


    // Start is called before the first frame update
    void Start()
    {
        myCamList.Add(camera1);
        myCamList.Add(camera2);
        myCamList.Add(camera3);
        myCamList.Add(camera4);
        myCamList.Add(camera5);
        SwitchToCam(camera1);
    }

    // Update is called once per frame
    void Update()
    {
        if (myKB.digit1Key.wasPressedThisFrame)
        {
            SwitchToCam(camera1);
        }

        if (myKB.digit2Key.wasPressedThisFrame)
        {
            SwitchToCam(camera2);
        }

        if (myKB.digit3Key.wasPressedThisFrame)
        {
            SwitchToCam(camera3);
        }

        if (myKB.digit4Key.wasPressedThisFrame)
        {
            SwitchToCam(camera4);
        }

        if (myKB.digit5Key.wasPressedThisFrame)
        {
            SwitchToCam(camera5);
        }
    }

    private void SwitchToCam(CinemachineVirtualCamera newActiveCam)
    {
        if(newActiveCam.Priority != 10)
        {
            foreach (CinemachineVirtualCamera cammy in myCamList)
            {
                cammy.Priority = 0;
            }
            newActiveCam.Priority = 10;
            cameraNameText.text = newActiveCam.gameObject.name;
        }
    }
}
