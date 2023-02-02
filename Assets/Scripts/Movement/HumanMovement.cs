using UnityEngine;

namespace Movement
{
    public class HumanMovement : MovementBase
    {
        [SerializeField] protected Transform feetTransform;
        [SerializeField] protected LayerMask floorMask;

        [SerializeField] protected float jumpForce;

        private void Update()
        {
            //Get Input
            MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            JumpCheck();
            RotatePlayer();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void JumpCheck()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(feetTransform.position, .25f, floorMask))
            {
                Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}