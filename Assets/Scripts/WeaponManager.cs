using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject sword;
    void Start()
    {
        // Start with the Gun active
        SelectWeapon(1);
    }

    void Update()
    {
        bool isDialogueActive = NPCUIManager.Instance != null && NPCUIManager.Instance.IsMenuBusy() == true;
        bool isGamePaused = PauseMenu.Instance!=null && PauseMenu.Instance.isPaused;

        // Prevent camera movement and interaction during UI elements
        if (isDialogueActive || isGamePaused)
        {
            return; 
        }
        
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectWeapon(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectWeapon(2);
    }

    void SelectWeapon(int weaponIndex)
    {
        // Toggle the objects based on selection
        gun.SetActive(weaponIndex == 1);
        sword.SetActive(weaponIndex == 2);
    }
}