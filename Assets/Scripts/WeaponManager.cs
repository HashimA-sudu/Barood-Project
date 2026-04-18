using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
    public GameObject gun;
    public GameObject sword;
    
    private int currentWeapon = 0; // 0 = none, 1-4 = weapon slot index
    private float fKeyHoldTime = 0f;
    private const float DESELECT_HOLD_TIME = 2f;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Start with NO weapons - they must be picked up
        if (gun != null) gun.SetActive(false);
        if (sword != null) sword.SetActive(false);
        currentWeapon = 0;
    }

    void Update()
    {
        // Check if controls are disabled
        if (ControlsManager.AreControlsDisabled())
        {
            return;
        }
        
        // Weapon selection by slot (1-4)
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectWeaponBySlot(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectWeaponBySlot(2);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectWeaponBySlot(3);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectWeaponBySlot(4);
        
        // F key hold to deselect weapon
        if (Keyboard.current.fKey.isPressed)
        {
            fKeyHoldTime += Time.deltaTime;
            if (fKeyHoldTime >= DESELECT_HOLD_TIME)
            {
                SelectWeapon(0);
                fKeyHoldTime = 0f;
            }
        }
        else
        {
            fKeyHoldTime = 0f;
        }
    }
    
    void SelectWeaponBySlot(int slotIndex)
    {
        // slotIndex is 1-4, check if that weapon exists in inventory
        if (InventoryManager.Instance == null || slotIndex < 1 || slotIndex > 4)
            return;
        
        // Check if weapon exists at this slot
        if (slotIndex <= InventoryManager.Instance.weapons.Count)
        {
            // Weapon exists, equip it
            SelectWeapon(slotIndex);
        }
        else
        {
            Debug.Log($"No weapon in slot {slotIndex}");
        }
    }

    void SelectWeapon(int weaponIndex)
    {
        currentWeapon = weaponIndex;
        
        // Disable both weapons first
        if (gun != null) gun.SetActive(false);
        if (sword != null) sword.SetActive(false);
        
        // If selecting a weapon slot, activate the correct weapon from inventory
        if (weaponIndex > 0 && weaponIndex <= InventoryManager.Instance.weapons.Count)
        {
            InventoryItem selectedItem = InventoryManager.Instance.weapons[weaponIndex - 1];
            
            if (selectedItem.itemName.ToLower().Contains("gun") && gun != null)
            {
                gun.SetActive(true);
            }
            else if (selectedItem.itemName.ToLower().Contains("sword") && sword != null)
            {
                sword.SetActive(true);
            }
        }
        // If weaponIndex is 0, no weapon is active (both stay disabled)
    }
    
    public void UnlockWeapon(string weaponName)
    {
        weaponName = weaponName.ToLower();
        
        // If no weapon is currently selected, equip this one
        if (currentWeapon == 0)
        {
            if (weaponName.Contains("gun"))
            {
                Debug.Log("Gun picked up and equipped!");
                SelectWeapon(1);
            }
            else if (weaponName.Contains("sword"))
            {
                Debug.Log("Sword picked up and equipped!");
                SelectWeapon(1); // Will be in first slot
            }
        }
        else
        {
            // Weapon added but not equipped (player already has one)
            Debug.Log($"{weaponName} picked up but not equipped. Press 1-4 to switch.");
        }
    }
    
    public bool HasWeapon(string weaponName)
    {
        weaponName = weaponName.ToLower();
        int weaponCount = InventoryManager.Instance.weapons.Count;
        
        if (weaponName.Contains("gun"))
        {
            return weaponCount > 0 && InventoryManager.Instance.weapons.Exists(w => w.itemName.ToLower().Contains("gun"));
        }
        else if (weaponName.Contains("sword"))
        {
            return weaponCount > 0 && InventoryManager.Instance.weapons.Exists(w => w.itemName.ToLower().Contains("sword"));
        }
        
        return false;
    }
}