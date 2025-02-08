using Interfaces;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void MakeDamageTo(GameObject obj)
    {
        if (obj.TryGetComponent(out IDestructible destructible))
        {
            destructible.TakeDamage();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
            MakeDamageTo(collision.gameObject);

        Destroy(gameObject);
    }
}
