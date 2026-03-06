using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    public float swingSpeed = 10f;
    private bool isSwinging = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isSwinging && (NPCUIManager.Instance == null || !NPCUIManager.Instance.IsMenuBusy() == true))
        {
            StartCoroutine(SwingRoutine());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f); // Set to 1 as requested
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