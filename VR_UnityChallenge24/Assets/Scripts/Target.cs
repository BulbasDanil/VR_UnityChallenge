using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour, IDestructible
{
    [SerializeField] private List<Material> Materials;
    
    private int _health = 5;
    private bool _canTakeDamage = true;
    private MeshRenderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _health = generateHealth();
        paintTheBalloon();
        setRandomYPos();
    }

    public void TakeDamage()
    {
        if (_canTakeDamage)
        {
            _health -= 1;
            paintTheBalloon();
            
            if (_health <= 0)
            {
                GameManager.Instance.BalloonDestroyed();
                StartCoroutine(DestroyBalloon());
            }
            else
            {
                StartCoroutine(DamageCooldown()); 
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        _canTakeDamage = true;
    }

    private void paintTheBalloon()
    {
        if(_health < Materials.Count && _renderer)
            _renderer.material = Materials[_health];
    }

    private int generateHealth()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < 20) return 1; 
        if (randomValue < 40) return 2;  
        if (randomValue < 60) return 3;  
        if (randomValue < 80) return 4;
        return 5;  
    }

    private void setRandomYPos()
    {
        Vector3 currentPosition = transform.position;
        
        float randomY = Random.Range(1.75f, 2.50f);
        
        transform.position = new Vector3(currentPosition.x, randomY, currentPosition.z);
    }

    private IEnumerator DestroyBalloon()
    {
        Vector3 targetPosition = transform.position + new Vector3(0f, 5f, 0f);
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb)
            rb.isKinematic = true;

        while (transform.position.y < targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
            yield return null; 
        }

        Destroy(gameObject);
    }
    
}
