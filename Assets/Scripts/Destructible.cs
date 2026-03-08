using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health = 1;
    public GameObject lootPrefab; //some object or loot to drop after destruction
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
