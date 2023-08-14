using UnityEngine;

namespace MiCompa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _acceleration = default;
        [SerializeField]
        private float _maxSpeed = default;

        private Rigidbody _rigidbody;

        public float Acceleration { get => _acceleration; set => _acceleration = value; }
        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        private void Awake()
        {
            if (TryGetComponent(out Rigidbody rigidbody)) _rigidbody = rigidbody;
        }

        public void MoveLocalZAxis(float input) => MoveInDirection(input * transform.forward);
        public void MoveWorldZAxis(float input) => MoveInDirection(input * Vector3.forward);
        public void MoveInDirection(Vector3 movementDirection)
        {
            var newVelocity = GetNewVelocity(_acceleration, _maxSpeed, movementDirection);
            _rigidbody.velocity = newVelocity;
        }
        public Vector3 GetNewVelocity(float acceleration, float maxMoveSpeed, Vector3 desiredMovement)
        {
            var currentVelocity = _rigidbody.velocity;
            var velocityDelta = acceleration * Time.fixedDeltaTime * desiredMovement;
            var newVelocity = currentVelocity + velocityDelta;

            var newVelocityMagnitude = newVelocity.magnitude;
            var newVelocityNormalized = newVelocity / newVelocityMagnitude;

            if (currentVelocity.sqrMagnitude <= maxMoveSpeed * maxMoveSpeed)
            {
                if (newVelocityMagnitude > maxMoveSpeed)
                {
                    newVelocity = newVelocityNormalized * maxMoveSpeed;
                }

                return newVelocity;
            }
            else
            {
                var currentVelocityMagnitude = currentVelocity.magnitude;
                var currentVelocityNormalized = currentVelocity / currentVelocityMagnitude;

                float desiredMovementAndCurrentVelocityDot = Vector3.Dot(desiredMovement, currentVelocityNormalized);
                bool areDesiredMovementAndCurrentVelocityAligned = desiredMovementAndCurrentVelocityDot >= 0f;
                if (areDesiredMovementAndCurrentVelocityAligned)
                {
                    if (newVelocityMagnitude > currentVelocityMagnitude)
                    {
                        newVelocity = newVelocityNormalized * currentVelocityMagnitude;
                    }
                }

                return newVelocity;
            }
        }
    }
}