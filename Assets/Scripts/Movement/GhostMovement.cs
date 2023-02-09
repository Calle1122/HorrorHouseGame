using UnityEngine;

namespace Movement
{
    public class GhostMovement : MovementBase
    {
        [SerializeField] private Vector2 floatRange;

        [SerializeField] private float floatSpeed;

        private float _desiredHeight;
        private Oscillator _osc;

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

            if (shouldJump)
            {
                _desiredHeight = floatRange.y;
                _osc._localEquilibriumPosition.y = Mathf.Lerp(_osc._localEquilibriumPosition.y, _desiredHeight,
                    Time.deltaTime * floatSpeed);
            }

            else
            {
                _desiredHeight = floatRange.x;
                _osc._localEquilibriumPosition.y = Mathf.Lerp(_osc._localEquilibriumPosition.y, _desiredHeight,
                    Time.deltaTime * floatSpeed);
            }

            RotatePlayer();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void OnEnable()
        {
            Game.CharacterHandler.OnGhostMovementInput.AddListener(OnGhostMovementInput);
            Game.CharacterHandler.OnGhostNoMovementInput.AddListener(OnGhostMovementInput);
            Game.CharacterHandler.OnGhostJumpPressed.AddListener(OnGhostJumpPressed);
            Game.CharacterHandler.OnGhostJumpReleased.AddListener(OnGhostJumpReleased);
        }

        private void OnDisable()
        {
            Game.CharacterHandler.OnGhostMovementInput.RemoveListener(OnGhostMovementInput);
            Game.CharacterHandler.OnGhostNoMovementInput.RemoveListener(OnGhostMovementInput);
            Game.CharacterHandler.OnGhostJumpPressed.RemoveListener(OnGhostJumpPressed);
            Game.CharacterHandler.OnGhostJumpReleased.RemoveListener(OnGhostJumpReleased);
        }

        private void OnGhostJumpPressed()
        {
            if (Game.CharacterHandler.GhostInputMode != InputMode.Free)
            {
                shouldJump = false;
                return;
            }

            shouldJump = true;
        }

        private void OnGhostJumpReleased()
        {
            shouldJump = false;
        }

        private void OnGhostMovementInput(Vector3 input)
        {
            if (Game.CharacterHandler.GhostInputMode != InputMode.Free)
            {
                MovementInput = Vector3.zero;
                return;
            }

            MovementInput = input;
        }
    }
}