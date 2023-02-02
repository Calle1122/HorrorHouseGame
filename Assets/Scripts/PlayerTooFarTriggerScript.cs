using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTooFarTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if both players are inside the collider

        // if not, do something...
    }

    private void OnTriggerExit(Collider other)
    {
        // for now let's just do this

        if(other.tag == "Player")
        {
            
            PlayerControllerScript2.tooFarBool = true;
        }
    }


}
