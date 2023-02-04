using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhettoAssMovementControllerScript : MonoBehaviour
{
    private Keyboard myKB;
    private Gamepad myGP;
    private Vector2 humanMoveInput = Vector2.zero;
    private Vector2 ghostMoveInput = Vector2.zero;
    public static bool humanInteract = false;
    public static bool ghostInteract = false;
    public static bool humanIsMoving = false;
    public static bool ghostIsMoving = false;
    public float moveSpeed = 1;

    [SerializeField] private GameObject humanObject;
    [SerializeField] private GameObject ghostObject;
    

    private void Awake()
    {
        myKB = Keyboard.current;
        myGP = Gamepad.current;
        if(myGP == null)
        {
            Debug.Log("No gamepad detected!");
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

        GetPlayerInputs();

        // debuging

        if (myKB.mKey.wasPressedThisFrame)
        {
            SFXDemoManager.ghostMovementBool = true;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacters();
    }

    private void GetPlayerInputs()
    {
        // get human input from keyboard

        // left right
        if (myKB.wKey.isPressed)
        {
            humanMoveInput.y = 1f;
        }
        else if (myKB.sKey.isPressed)
        {
            humanMoveInput.y = -1f;
        }
        else
        {
            humanMoveInput.y = 0;
        }

        // forward back
        if (myKB.dKey.isPressed)
        {
            humanMoveInput.x = 1f;
        }
        else if (myKB.aKey.isPressed)
        {
            humanMoveInput.x = -1f;
        }
        else
        {
            humanMoveInput.x = 0;
        }

        if(Mathf.Abs(humanMoveInput.magnitude) != 0)
        {
            humanIsMoving = true;
        } else
        {
            humanIsMoving = false;
        }

        if(Mathf.Abs(ghostMoveInput.magnitude) != 0)
        {
            ghostIsMoving = true;
        } else
        {
            ghostIsMoving = false;
        }

        //interact
        if (myKB.eKey.wasPressedThisFrame)
        {
            // do something
        }

        // get ghost input from gamepad
        ghostMoveInput = myGP.leftStick.ReadValue();

        //interact
        if (myGP.buttonSouth.wasPressedThisFrame)
        {
            // do something
        }

    }

    private void MoveCharacters()
    {
        Vector3 humanMovementVector = new Vector3(humanMoveInput.x, 0, humanMoveInput.y);
        Vector3 ghostMovementVector = new Vector3(ghostMoveInput.x, 0, ghostMoveInput.y);

        humanObject.transform.Translate(humanMovementVector * Time.fixedDeltaTime * moveSpeed);
        ghostObject.transform.Translate(ghostMovementVector * Time.fixedDeltaTime * moveSpeed);
    }
}
