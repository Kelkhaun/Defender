using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(DOTweenPath))]
[RequireComponent(typeof(Animator))]
public class Princess : MonoBehaviour
{
    private DOTweenPath _path;
    private Animator _animator;

    public event Action Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _path = GetComponent<DOTweenPath>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Bullet bullet))
            Die();
    }

    private void Die()
    {
        Dying?.Invoke();
        _animator.Play(PrincessAnimator.States.Death);
        _path.DOPause();

        float gameSpeedAfterDeath = 0.6f;
        Time.timeScale = gameSpeedAfterDeath;
    }
}
