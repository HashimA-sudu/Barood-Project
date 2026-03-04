using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        // Destroy the bullet after a few seconds so we don't leak memory
        Destroy(gameObject, lifeTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Inside OnTriggerEnter
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f); // Set to 1 as requested
            }
            
            // If it's a bullet, destroy the bullet on hit
            if (this.gameObject.name.Contains("Bullet")) Destroy(gameObject); 
        }
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}