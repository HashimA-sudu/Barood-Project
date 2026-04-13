using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryMenu;
    public Transform itemContainer;
    public GameObject slotPrefab;

    public static bool isOpen = false;

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (isOpen) CloseInventory();
            else OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryMenu.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f; // Freeze the world
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        RefreshUI();
    }

    public void CloseInventory()
    {
        inventoryMenu.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RefreshUI()
    {
        // 1. Clear the old slots
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // 2. Add Weapon Icons
        foreach (InventoryItem item in InventoryManager.Instance.weapons)
        {
            CreateSlot(item);
        }

        // 3. Add Item Icons
        foreach (InventoryItem item in InventoryManager.Instance.items)
        {
            CreateSlot(item);
        }
    }

    void CreateSlot(InventoryItem item)
    {
        GameObject newSlot = Instantiate(slotPrefab, itemContainer);
        // Find the 'Icon' child and set its sprite
        UnityEngine.UI.Image iconImage = newSlot.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
        iconImage.sprite = item.icon;
    }
}