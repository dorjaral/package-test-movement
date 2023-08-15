using UnityEngine;

namespace MiCompa.Movement.Samples
{
    [RequireComponent(typeof(MovementBehaviour))]
    public class MovingCapsule : MonoBehaviour
    {
        private MovementBehaviour _movementBehaviour;

        private void Awake()
        {
            if (TryGetComponent(out MovementBehaviour movementBehaviour)) _movementBehaviour = movementBehaviour;
        }
        private void FixedUpdate()
        {
            _movementBehaviour.MoveInDirection(Vector3.forward);
        }
    }
}