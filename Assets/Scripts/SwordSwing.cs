using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    public float swingSpeed = 10f;
    private bool isSwinging = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isSwinging && !ControlsManager.AreControlsDisabled())
        {
            StartCoroutine(SwingRoutine());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Check for enemies
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f); // Take 1 damage from sword
            }
        }
        
        // Check for destructibles
        if (other.CompareTag("Destructible"))
        {
            Destructible destructible = other.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.TakeDamage(1); // Take 1 damage
            }
        }
    }

    IEnumerator SwingRoutine()
    {
        isSwinging = true;
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = Quaternion.Euler(90, 0, 0) * startRot; // Swings 90 degrees

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        // Return to original position
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Lerp(endRot, startRot, t);
            yield return null;
        }

        isSwinging = false;
    }
}