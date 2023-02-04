using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BradsSFXEventsScript : MonoBehaviour
{
    private Keyboard myKB;
    private Gamepad myGP;
    [SerializeField] GameObject humanObject;
    [SerializeField] GameObject ghostObject;

    private bool humanIsMoving = false;
    private bool ghostIsMoving = false;

    private void Awake()
    {
        myKB = Keyboard.current;
        myGP = Gamepad.current;
        if(myGP == null)
        {
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckForMovement();
    }

    private void CheckForMovement()
    {
        if (GhettoAssMovementControllerScript.humanIsMoving)
        {
            SFXDemoManager.humanFootstepsBool = true;
        } else
        {
            SFXDemoManager.humanFootstepsBool = false;
        }

        if (GhettoAssMovementControllerScript.ghostIsMoving)
        {
            SFXDemoManager.ghostMovementBool = true;
        } else
        {
            SFXDemoManager.ghostMovementBool = false;
        }
    }
}
