using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryMenu;
    
    [Header("Weapons Section")]
    public Transform weaponContainer;
    
    [Header("Items Section")]
    public Transform itemContainer;
    
    public GameObject slotPrefab;

    public static bool isOpen = false;
    public static InventoryUI Instance;

    void Awake()
    {
        Instance = this;
        
        // Validate references
        if (inventoryMenu == null)
        {
            Debug.LogError("InventoryUI: inventoryMenu not assigned in inspector!");
        }
        if (weaponContainer == null)
        {
            Debug.LogError("InventoryUI: weaponContainer not assigned in inspector!");
        }
        if (itemContainer == null)
        {
            Debug.LogError("InventoryUI: itemContainer not assigned in inspector!");
        }
        if (slotPrefab == null)
        {
            Debug.LogError("InventoryUI: slotPrefab not assigned in inspector!");
        }
        
        // Ensure menu is off at start
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
        }
    }

    void Update()
    {
        // Listen for 'I' key to toggle inventory
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            // If inventory is open, close it
            if (isOpen)
            {
                CloseInventory();
            }
            // Otherwise open it only if controls aren't disabled by something else
            else if (!ControlsManager.AreControlsDisabled())
            {
                OpenInventory();
            }
        }
        
        // Listen for ESC key to close inventory (takes priority)
        if (Keyboard.current.escapeKey.wasPressedThisFrame && isOpen)
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        if (inventoryMenu == null) return;
        
        inventoryMenu.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f; // Freeze the world
        ControlsManager.SetInventoryOpen(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        RefreshUI();
    }

    public void CloseInventory()
    {
        if (inventoryMenu == null) return;
        
        inventoryMenu.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;
        ControlsManager.SetInventoryOpen(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RefreshUI()
    {
        if (slotPrefab == null || InventoryManager.Instance == null)
        {
            Debug.LogWarning("InventoryUI: Missing required references for RefreshUI");
            return;
        }

        // 1. Clear weapon slots
        if (weaponContainer != null)
        {
            foreach (Transform child in weaponContainer)
            {
                Destroy(child.gameObject);
            }

            // 2. Add Weapon Icons (always show 4 slots)
            foreach (InventoryItem item in InventoryManager.Instance.weapons)
            {
                CreateSlot(item, weaponContainer);
            }
            
            // Fill empty slots for weapons
            int weaponsToShow = InventoryManager.Instance.maxWeapons;
            while (weaponContainer.childCount < weaponsToShow)
            {
                CreateEmptySlot(weaponContainer);
            }
        }

        // 3. Clear item slots
        if (itemContainer != null)
        {
            foreach (Transform child in itemContainer)
            {
                Destroy(child.gameObject);
            }

            // 4. Add Item Icons (only show items that exist - no empty slots)
            foreach (InventoryItem item in InventoryManager.Instance.items)
            {
                CreateSlot(item, itemContainer);
            }
        }
    }

    void CreateSlot(InventoryItem item, Transform container)
    {
        GameObject newSlot = Instantiate(slotPrefab, container);
        
        // Find and set the Icon image
        Transform iconTransform = newSlot.transform.Find("Icon");
        if (iconTransform != null)
        {
            UnityEngine.UI.Image iconImage = iconTransform.GetComponent<UnityEngine.UI.Image>();
            if (iconImage != null && item.icon != null)
            {
                iconImage.sprite = item.icon;
            }
        }
        
        // Find and set the Item Name text
        Transform nameTransform = newSlot.transform.Find("ItemName");
        if (nameTransform != null)
        {
            TMPro.TextMeshProUGUI nameText = nameTransform.GetComponent<TMPro.TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = item.itemName;
            }
        }
    }
    
    void CreateEmptySlot(Transform container)
    {
        GameObject emptySlot = Instantiate(slotPrefab, container);
        
        // Find and clear the Icon image
        Transform iconTransform = emptySlot.transform.Find("Icon");
        if (iconTransform != null)
        {
            UnityEngine.UI.Image iconImage = iconTransform.GetComponent<UnityEngine.UI.Image>();
            if (iconImage != null)
            {
                iconImage.sprite = null;
            }
        }
        
        // Find and clear the Item Name text
        Transform nameTransform = emptySlot.transform.Find("ItemName");
        if (nameTransform != null)
        {
            TMPro.TextMeshProUGUI nameText = nameTransform.GetComponent<TMPro.TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = "";
            }
        }
    }
}