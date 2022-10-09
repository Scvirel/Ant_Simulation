using UnityEngine;

namespace AntSimulation.Client
{
    public sealed class AntBehaviour : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 2f;
        [SerializeField] private float _steerStrength = 2f;
        [SerializeField] private Transform _target = default;

        private Vector2 _currentPosition = default;
        private Vector2 _currentVelocity = default;
        private Vector2 _desiredDirection = default;

        private void Update()
        {
            _desiredDirection = ((Vector2)_target.position - _currentPosition).normalized;

            Vector2 desiredVelocity = _desiredDirection * _maxSpeed;
            Vector2 desiredSteeringForce = (desiredVelocity - _currentVelocity) * _steerStrength;
            Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, _steerStrength) / 1;

            _currentVelocity = Vector2.ClampMagnitude(_currentVelocity + acceleration * Time.deltaTime, _maxSpeed);
            _currentPosition += _currentVelocity * Time.deltaTime;

            float angle = Mathf.Atan2(_currentVelocity.y, _currentVelocity.x) * Mathf.Rad2Deg;
            transform.SetPositionAndRotation(_currentPosition, Quaternion.Euler(0, 0, angle));
        }
    }
}