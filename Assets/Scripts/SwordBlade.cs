using UnityEngine;

public class SwordBlade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing we touched is an Enemy
        if (other.CompareTag("Enemy"))
        {
            // 2. Get the health script from the enemy
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(1f);
                Debug.Log("Blade Hit!");
            }

        }
        else if (other.CompareTag("Destructible"))
        {
            Destructible crate = other.GetComponent<Destructible>();
            if(crate != null)
            {
                crate.TakeDamage(1);
                Debug.Log("Blade Hit Destructible!");
            }
        }
    }
}