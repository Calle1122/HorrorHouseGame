using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{
    public float playerMoveSpeed = 10f;

    [SerializeField] GameObject playerOneObject;
    [SerializeField] GameObject playerOneModel;
    private Rigidbody playerOneRB;
    

    private Gamepad myGP;
    private Keyboard myKB;

    private Vector2 moveInput;

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
        playerOneRB = playerOneObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // gamepad inputs
        if(myGP != null)
        {
            moveInput = myGP.leftStick.ReadValue();
        } else // keyboard inputs
        {

            //left-right
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
            //forward-back
            if (myKB.wKey.isPressed)
            {
                moveInput.y = 1f;
            } else if (myKB.sKey.isPressed)
            {
                moveInput.y = -1f;
            } else
            {
                moveInput.y = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        playerOneModel.transform.localPosition = Vector3.zero;
        Vector3 playerMoveVector = new Vector3(moveInput.x, 0f, moveInput.y);
        
        

        playerOneRB.velocity = playerMoveVector * playerMoveSpeed;

        if(playerMoveVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerMoveVector);
            targetRotation = Quaternion.RotateTowards(playerOneModel.transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
            playerOneModel.GetComponent<Rigidbody>().MoveRotation(targetRotation);
        }
        
    }

}
