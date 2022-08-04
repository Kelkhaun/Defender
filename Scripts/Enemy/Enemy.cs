using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Pistol _pistol;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Rigidbody[] _rigidbodies;

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private Coroutine _shoot;
    private float _timeBetweenShots = 1.29f;
    private float _throwForce = 0.15f;

    private void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();

        _shoot = StartCoroutine(Shoot());

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Bullet bullet))
            Die();
    }

    private void Die()
    {
        MakePhysical();
        _boxCollider.isTrigger = false;

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].AddForce(transform.forward * _throwForce, ForceMode.Impulse);
        }

        StopCoroutine(_shoot);
        _pistol.StopShooting();
        ChangeColor();
    }

    private void MakePhysical()
    {
        _animator.enabled = false;

        for (int i = 0; i < _rigidbodies.Length; i++)
            _rigidbodies[i].isKinematic = false;
    }

    private void ChangeColor()
    {
        float colorShade1 = 0.65f;
        float colorShade2 = 0.2f;
        float colorShade3 = 0.34f; 

        Color mainColor = new Color(colorShade1, colorShade1, colorShade1);
        Color colorDimExtra = new Color(colorShade2, colorShade2, colorShade2);
        Color flatRimColor = new Color(colorShade3, colorShade3, colorShade3);

        _skinnedMeshRenderer.material.SetColor("_Color", mainColor);
        _skinnedMeshRenderer.material.SetColor("_ColorDimExtra", colorDimExtra);
        _skinnedMeshRenderer.material.SetColor("_FlatRimColor", flatRimColor);
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenShots);

        while (true)
        {
            _pistol.Shoot(_shootPoint, transform.rotation);
            yield return waitForSeconds;
        }
    }
}
