using UnityEngine;

namespace Movement
{
    public abstract class MovementBase : MonoBehaviour
    {
        public bool pressingJump;

        [SerializeField] protected float moveSpeed;
        protected Vector3 MovementInput;
        protected Rigidbody Rb;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
        }

        protected void MovePlayer()
        {
            var moveDirection = MovementInput * moveSpeed;
            Rb.velocity = new Vector3(moveDirection.x, Rb.velocity.y, moveDirection.z);
        }

        protected void RotatePlayer()
        {
            if (MovementInput != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(MovementInput);
                targetRotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
                Rb.MoveRotation(targetRotation);
            }
        }
    }
}