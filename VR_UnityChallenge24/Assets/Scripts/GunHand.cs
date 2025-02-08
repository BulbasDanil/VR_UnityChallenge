using UnityEngine;

public class GunHand : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private float ProjectileSpeed = 5f;
    
    public void Shoot(OVRHand Hand)
    {
        if (Physics.Raycast(Hand.PointerPose.position, Hand.PointerPose.forward, out RaycastHit hit, Mathf.Infinity, Mask))
        {
            if (LineRenderer)
            {
                UpdateRayVisualization(Hand.PointerPose.position, hit.point, true);    
            }
            
            
            GameObject newProjectile = Instantiate(Projectile, Hand.PointerPose.position, Quaternion.identity);
            
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 direction = (hit.point - Hand.PointerPose.position).normalized;
                rb.velocity = direction * ProjectileSpeed;
            }
        }
    }

    private void UpdateRayVisualization(Vector3 startPosition, Vector3 endPosition, bool hitSomething)
    {
        if (LineRenderer != null)
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, startPosition);
            LineRenderer.SetPosition(1, endPosition);
            LineRenderer.material.color = hitSomething ? Color.green : Color.red;
        }
        else if (LineRenderer != null)
        {
            LineRenderer.enabled = false;
        }
    }
}
