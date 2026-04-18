using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health = 1;
    public GameObject lootPrefab; //some object or loot to drop after destruction
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Destructible taking {damage} damage. Health now: {health}");
        
        if (health <= 0)
        {
            Debug.Log("Destructible destroyed!");
            
            if (lootPrefab != null)
            {
                // Get the center of the collider to spawn at the actual object position
                Vector3 spawnPosition = transform.position;
                
                Collider col = GetComponent<Collider>();
                if (col != null)
                {
                    spawnPosition = col.bounds.center;
                }
                
                Debug.Log($"Spawning loot: {lootPrefab.name}");
                GameObject spawnedLoot = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"Loot spawned at {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("Destructible: lootPrefab is null! Please assign it in the inspector.");
            }
            
            Destroy(gameObject);
        }
    }
}
