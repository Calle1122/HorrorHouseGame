using Animation;
using GameConstants;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(AnimationsHandler))]
    public class GhostMovement : MovementBase
    {
        public Vector2 floatRange;

        [SerializeField] private float floatSpeed;

        private float _desiredHeight;
        private Oscillator _osc;
        private AnimationsHandler animationsHandler;

        protected override void Awake()
        {
            base.Awake();
            animationsHandler = GetComponent<AnimationsHandler>();
        }

        private void Start()
        {
            _osc = GetComponent<Oscillator>();
            _desiredHeight = floatRange.x;
        }

        private void Update()
        {
            _osc._localEquilibriumPosition.x = transform.position.x;
            _osc._localEquilibriumPosition.z = transform.position.z;

            //FLOATING:

            if (pressingJump)
            {
                animationsHandler.SetBool(Strings.JumpParam, true);
                _desiredHeight = floatRange.y;
                _osc._localEquilibriumPosition.y = Mathf.Lerp(_osc._localEquilibriumPosition.y, _desiredHeight,
                    Time.deltaTime * floatSpeed);
            }

            else
            {
                animationsHandler.SetBool(Strings.JumpParam, false);
                _desiredHeight = floatRange.x;
                _osc._localEquilibriumPosition.y = Mathf.Lerp(_osc._localEquilibriumPosition.y, _desiredHeight,
                    Time.deltaTime * floatSpeed);
            }

            RotatePlayer();
        }

        private void FixedUpdate()
        {
            MovePlayer();
            if (MovementInput == Vector3.zero)
            {
                animationsHandler.SetBool(Strings.WalkParam, false);
            }
            else if (MovementInput != Vector3.zero)
            {
                animationsHandler.SetBool(Strings.WalkParam, true);
            }
        }

        private void OnEnable()
        {
            Game.Input.OnGhostMovementInput.AddListener(OnGhostMovementInput);
            Game.Input.OnGhostNoMovementInput.AddListener(OnGhostMovementInput);
            Game.Input.OnGhostJumpPressed.AddListener(OnGhostJumpPressed);
            Game.Input.OnGhostJumpReleased.AddListener(OnGhostJumpReleased);
        }

        private void OnDisable()
        {
            Game.Input.OnGhostMovementInput.RemoveListener(OnGhostMovementInput);
            Game.Input.OnGhostNoMovementInput.RemoveListener(OnGhostMovementInput);
            Game.Input.OnGhostJumpPressed.RemoveListener(OnGhostJumpPressed);
            Game.Input.OnGhostJumpReleased.RemoveListener(OnGhostJumpReleased);
        }

        private void OnGhostJumpPressed()
        {
            if (Game.Input.GhostInputMode != InputMode.Free)
            {
                pressingJump = false;
                return;
            }

            pressingJump = true;
        }

        private void OnGhostJumpReleased()
        {
            pressingJump = false;
        }

        private void OnGhostMovementInput(Vector3 input)
        {
            if (Game.Input.GhostInputMode != InputMode.Free)
            {
                MovementInput = Vector3.zero;
                return;
            }

            MovementInput = input;
        }
    }
}