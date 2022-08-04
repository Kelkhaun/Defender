using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pistol : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;
    private float _throwForce = 4f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StopShooting()
    {
        _animator.Play(PistolAnimator.States.Idle);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.forward * _throwForce, ForceMode.Impulse);
    }

    public void Shoot(Transform shootPoint, Quaternion rotation)
    {
        Instantiate(_bullet, shootPoint.transform.position, rotation);
    }
}
