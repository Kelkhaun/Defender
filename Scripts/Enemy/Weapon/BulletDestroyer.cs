using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Bullet bullet))
            Destroy(bullet.gameObject);
    }
}
