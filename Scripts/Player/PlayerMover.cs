using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private int _motionState;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetInteger(PlayerAnimator.States.MotionState, _motionState);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _motionState = (int)AnimationState.Run;
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + touch.deltaPosition.x * _movementSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z + touch.deltaPosition.y * _movementSpeed);
                TurnAround();
            }
        }
        else
        {
            float zeroVelocity = 0f;
            _motionState = (int)AnimationState.Idle;
            _rigidbody.velocity = new Vector3(zeroVelocity, _rigidbody.velocity.y, zeroVelocity);
        }
    }

    private void TurnAround()
    {
        Quaternion rotationAngle = Quaternion.LookRotation(-_rigidbody.velocity, Vector3.up);
        float duration = 0.5f;
        transform.DOLocalRotateQuaternion(rotationAngle, duration);
    }

    private enum AnimationState
    {
        Idle = 0,
        Run = 1
    }
}
