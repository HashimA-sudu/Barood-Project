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
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f); // Assuming each bullet does 1 damage
            }
            
            // If it's a bullet, destroy the bullet on hit
            if (this.gameObject.name.Contains("Bullet")) Destroy(gameObject); 
        }
        else if (other.CompareTag("Destructible"))
        {
            Destructible destructible = other.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.TakeDamage(1); // Assuming destructibles take 1 damage per hit
                Debug.Log("Bullet Hit Destructible!");
            }
            
            // If it's a bullet, destroy the bullet on hit
            if (this.gameObject.name.Contains("Bullet")) Destroy(gameObject); 
        }
         else if (other.CompareTag("Wall"))
        {
            // If it's a bullet, destroy the bullet on hit
            if (this.gameObject.name.Contains("Bullet")) Destroy(gameObject); 
        }
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}