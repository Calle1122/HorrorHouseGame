using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BradsTempControllerScript : MonoBehaviour
{
    private Keyboard myKB;
    private Vector2 inputVector_1 = Vector2.zero;
    public float moveSpeed_1 = .5f;
    private Vector2 inputVector_2 = Vector2.zero;
    public float moveSpeed_2 = .5f;

    public static bool QTEInProgress = false;
    public static bool invertControls = false; 

    [SerializeField] private GameObject playerObject_1;
    [SerializeField] private GameObject playerObject_2;

    private void Awake()
    {
        myKB = Keyboard.current;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!QTEInProgress)
        {
            GetPlayerOneInputs();
            GetPlayerTwoInputs();
        }
        
        
    }

    private void FixedUpdate()
    {
        Vector3 playerMoveVector_1 = new Vector3(inputVector_1.x, 0, inputVector_1.y);
        Vector3 playerMoveVector_2 = new Vector3(inputVector_2.x, 0, inputVector_2.y);

        playerObject_1.transform.Translate(playerMoveVector_1 * moveSpeed_1);
        playerObject_2.transform.Translate(playerMoveVector_2 * moveSpeed_2);
    }

    private void GetPlayerOneInputs()
    {
        if (myKB.dKey.isPressed)
        {
            inputVector_1.x = 1;
        }
        else if (myKB.aKey.isPressed)
        {
            inputVector_1.x = -1;
        }
        else
        {
            inputVector_1.x = 0;
        }

        // F-B
        if (myKB.wKey.isPressed)
        {
            inputVector_1.y = 1;
        }
        else if (myKB.sKey.isPressed)
        {
            inputVector_1.y = -1;
        }
        else
        {
            inputVector_1.y = 0;
        }

        
        if (invertControls)
        {
            inputVector_1 *= -1;
        }
        
    }

    private void GetPlayerTwoInputs()
    {
        if (myKB.lKey.isPressed)
        {
            inputVector_2.x = 1;
        }
        else if (myKB.jKey.isPressed)
        {
            inputVector_2.x = -1;
        }
        else
        {
            inputVector_2.x = 0;
        }

        // F-B
        if (myKB.iKey.isPressed)
        {
            inputVector_2.y = 1;
        }
        else if (myKB.kKey.isPressed)
        {
            inputVector_2.y = -1;
        }
        else
        {
            inputVector_2.y = 0;
        }

        
        if (invertControls)
        {
            inputVector_2 *= -1;
        }
        
    }




}
