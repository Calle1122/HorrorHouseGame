using System.Collections;
using Animation;
using GameConstants;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(AnimationsHandler))]
    public class HumanMovement : MovementBase
    {
        [SerializeField] protected Transform feetTransform;
        [SerializeField] protected LayerMask floorMask;
        [SerializeField] protected float jumpForce;
        private bool isOnCooldown;

        private AnimationsHandler animationsHandler;

        protected override void Awake()
        {
            base.Awake();
            animationsHandler = GetComponent<AnimationsHandler>();
        }

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
            if (input == Vector3.zero)
            {
                animationsHandler.SetBool(Strings.WalkParam, false);
            }
            else if (input != Vector3.zero)
            {
                animationsHandler.SetBool(Strings.WalkParam, true);
            }
        }

        private void OnHumanJumpPressed()
        {
            if (Game.Input.HumanInputMode != InputMode.Free)
            {
                pressingJump = false;
                return;
            }

            pressingJump = true;
        }

        private void OnHumanJumpReleased()
        {
            pressingJump = false;
        }

        private void JumpCheck()
        {
            if (!CanJumpPhysicsCheck())
            {
                return;
            }

            if (!pressingJump || isOnCooldown)
            {
                return;
            }

            animationsHandler.TriggerJump();
            var rbVelocity = Rb.velocity;
            Rb.velocity = new Vector3(rbVelocity.x, 0, rbVelocity.z);
            Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(ResetJumpCooldown());
        }

        private bool CanJumpPhysicsCheck()
        {
            return Physics.CheckSphere(feetTransform.position, .25f, floorMask);
        }

        private IEnumerator ResetJumpCooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(0.5f);
            isOnCooldown = false;
        }
    }
}