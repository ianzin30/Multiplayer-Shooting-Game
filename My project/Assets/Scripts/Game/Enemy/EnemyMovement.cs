using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _rotationSpeed = 100;
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection() {
        if (_playerAwarenessController.AwareOfPlayer) {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;

        } else {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget() {
        if (_targetDirection == Vector2.zero) {
            return ;

        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity() {
        if (_targetDirection == Vector2.zero) {
            _rigidbody.velocity = Vector2.zero;

        } else {
            _rigidbody.velocity = transform.up * _speed;
        }
    }
}