using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        bool isDialogueActive = NPCUIManager.Instance != null && NPCUIManager.Instance.IsMenuBusy() == true;
        bool isGamePaused = PauseMenu.Instance!=null && PauseMenu.Instance.isPaused;

        if (Input.GetButtonDown("Fire1") && !(isGamePaused || isDialogueActive)) // busy menu/dialogue, dont shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}