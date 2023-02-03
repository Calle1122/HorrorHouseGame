using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;

public class TempControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    public string triggeringTag = "Player1";
    public float playerMoveSpeed = 5f;
    private Transform playerTransform;
    private Vector2 moveInput = Vector2.zero;

    [SerializeField] private CinemachineVirtualCamera startCam;
    [SerializeField] private CinemachineVirtualCamera Camera1;
    [SerializeField] private CinemachineVirtualCamera Camera2;
    [SerializeField] private CinemachineVirtualCamera Camera3;
    [SerializeField] private CinemachineVirtualCamera Camera4;
    [SerializeField] private CinemachineVirtualCamera Camera5;
    [SerializeField] private CinemachineVirtualCamera Camera6;
    List<CinemachineVirtualCamera> allMyCams = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera currentActiveCam;
    public string activeCameraTag = "ActiveCam";

    private Keyboard myKB;

    private void Awake()
    {
        

        playerTransform = playerObject.transform;
        myKB = Keyboard.current;
    }


    // Start is called before the first frame update
    void Start()
    {
        allMyCams.Add(Camera1);
        allMyCams.Add(Camera2);
        allMyCams.Add(Camera3);
        allMyCams.Add(Camera4);
        allMyCams.Add(Camera5);
        allMyCams.Add(Camera6);
        SwitchToCam(startCam);
        currentActiveCam = startCam;
    }

    // Update is called once per frame
    void Update()
    {
        // east-west
        if (myKB.dKey.isPressed)
        {
            moveInput.x = 1f;
        } else if (myKB.aKey.isPressed)
        {
            moveInput.x = -1f;
        } else
        {
            moveInput.x = 0f;
        }

        // east-west
        if (myKB.wKey.isPressed)
        {
            moveInput.y = 1f;
        }
        else if (myKB.sKey.isPressed)
        {
            moveInput.y = -1f;
        }
        else
        {
            moveInput.y = 0f;
        }

        if (myKB.digit6Key.isPressed)
        {
            
            Camera6.Priority = 11;
        } else
        {
            Camera6.Priority = 0;
        }


        SwitchToCam(currentActiveCam);
    }

    private void FixedUpdate()
    {
        Vector3 playerMoveVector = new Vector3(moveInput.x, 0, moveInput.y) * playerMoveSpeed;
        playerTransform.Translate(playerMoveVector,Space.World);
    }

    private void SwitchToCam(CinemachineVirtualCamera newActiveCam)
    {
        if(newActiveCam.gameObject.tag != activeCameraTag)
        {
            foreach(CinemachineVirtualCamera cammy in allMyCams)
            {
                cammy.gameObject.tag = "Untagged";
                cammy.Priority = 0;
            }
            newActiveCam.gameObject.tag = activeCameraTag;
            newActiveCam.Priority = 10;
        }
    }

}
