using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1,10)][SerializeField] private float _movementSpeed;

    private float _delayBetweenShots = 5f;

    private void Update()
    {
        transform.Translate(Vector3.back * _movementSpeed * Time.deltaTime);

        Destroy(gameObject, _delayBetweenShots);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Shield shield))
            _movementSpeed = -_movementSpeed;
    }
}
