using System.Collections;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Princess _princess;
    [SerializeField] private Vector3 _forwardDirection;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _angle;
    [SerializeField] private float _distance;
    [SerializeField] private float _maxVectorLenght;

    private bool _isAlive = true;
    private Vector3 _nextPosition;
    private Coroutine _followThePrincess;

    private void OnEnable()
    {
        _princess.Dying += ShowPrincess;
    }

    private void OnDisable()
    {
        _princess.Dying -= ShowPrincess;
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(_angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        if (_isAlive)
        {
            _nextPosition = _player.position + Vector3.ClampMagnitude(_player.velocity, _maxVectorLenght);
            _nextPosition += Vector3.up * Mathf.Cos(Mathf.Deg2Rad * _angle) * _distance;
            _nextPosition += -_forwardDirection * Mathf.Sin(Mathf.Deg2Rad * _angle) * _distance;
            transform.position = Vector3.Lerp(transform.position, _nextPosition, _movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void ShowPrincess()
    {
        _isAlive = false;

        if (_followThePrincess == null)
            _followThePrincess = StartCoroutine(FollowThePrincess());
    }

    private IEnumerator FollowThePrincess()
    {
        float offsetX = 0.61f;
        float offsetY = 7.5f;
        float offsetZ = 5.16f;
        float angleX = 64.6f;
        float duration = 6f;

        Vector3 nextPosition = new Vector3(_princess.transform.position.x + offsetX, _princess.transform.position.y + offsetY, _princess.transform.position.z - offsetZ);
        Quaternion nextRotation = Quaternion.Euler(angleX, transform.rotation.y, transform.rotation.z);

        while (transform.position != nextPosition)
        {
            transform.position = Vector3.Lerp(transform.position, nextPosition, duration * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, duration * Time.deltaTime);

            yield return null;
        }
    }
}
