using System.Collections;
using UnityEngine;

namespace Movement
{
    public class HumanMovement : MovementBase
    {
        [SerializeField] protected Transform feetTransform;
        [SerializeField] protected LayerMask floorMask;

        [SerializeField] protected float jumpForce;

        private bool _canJump = true;

        private void Update()
        {
            JumpCheck();
            RotatePlayer();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void OnEnable()
        {
            Game.Input.OnHumanMovementInput.AddListener(OnHumanMovementInput);
            Game.Input.OnHumanNoMovementInput.AddListener(OnHumanMovementInput);
            Game.Input.OnHumanJumpPressed.AddListener(OnHumanJumpPressed);
            Game.Input.OnHumanJumpReleased.AddListener(OnHumanJumpReleased);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanMovementInput.RemoveListener(OnHumanMovementInput);
            Game.Input.OnHumanNoMovementInput.RemoveListener(OnHumanMovementInput);
            Game.Input.OnHumanJumpPressed.RemoveListener(OnHumanJumpPressed);
            Game.Input.OnHumanJumpReleased.RemoveListener(OnHumanJumpReleased);
        }

        private void OnHumanMovementInput(Vector3 input)
        {
            if (Game.Input.HumanInputMode != InputMode.Free)
            {
                MovementInput = Vector3.zero;
                return;
            }

            MovementInput = input;
        }

        private void OnHumanJumpPressed()
        {
            if (Game.Input.HumanInputMode != InputMode.Free)
            {
                shouldJump = false;
                return;
            }

            shouldJump = true;
        }

        private void OnHumanJumpReleased()
        {
            shouldJump = false;
        }

        private void JumpCheck()
        {
            if (shouldJump && Physics.CheckSphere(feetTransform.position, .25f, floorMask) && _canJump)
            {
                Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                StartCoroutine(ResetJumpCooldown());
            }
        }

        private IEnumerator ResetJumpCooldown()
        {
            _canJump = false;
            yield return new WaitForSeconds(.2f);
            _canJump = true;
        }
    }
}