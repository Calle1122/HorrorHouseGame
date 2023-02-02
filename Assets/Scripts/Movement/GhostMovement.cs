using UnityEngine;
using GD.MinMaxSlider;

namespace Movement
{
    public class GhostMovement : MovementBase
    {
        [MinMaxSlider(0, 10)] [SerializeField] private Vector2 floatRange;

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

            //Get Input
            MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.Space))
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
    }
}