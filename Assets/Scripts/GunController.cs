using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (NPCUIManager.Instance == null || !NPCUIManager.Instance.IsMenuBusy() == true)) // Default is Left-Click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}