using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Current Storage")]
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<InventoryItem> weapons = new List<InventoryItem>();

    [Header("Capacity Limits")]
    public int maxItems = 10;
    public int maxWeapons = 4;

    public TMPro.TextMeshProUGUI notificationText;  

    void Awake() 
    { 
        Instance = this;
        // Clear inventory on start - items must be picked up
        items.Clear();
        weapons.Clear();
    }

    public bool AddItem(InventoryItem item)
    {
        if (item == null)
        {
            Debug.LogWarning("InventoryManager: Trying to add null item!");
            return false;
        }
        
        if (item.isWeapon)
        {
            if (weapons.Count >= maxWeapons)
            {
                ShowNotification("Weapon Slots Full!");
                return false;
            }
            weapons.Add(item);
            ShowNotification($"Picked up {item.itemName}");
            return true;
        }
        else
        {
            if (items.Count >= maxItems)
            {
                ShowNotification("Inventory Full!");
                return false;
            }
            items.Add(item);
            ShowNotification($"Picked up {item.itemName}");
            return true;
        }
    }

    public void ExpandInventory(int extraSlots)
    {
        maxItems += extraSlots;
        ShowNotification("Inventory Expanded!");
    }

    void ShowNotification(string message)
    {
        if (notificationText != null)
        {
            notificationText.text = message;
        }
    }

    void HideNotification() 
    { 
        if (notificationText != null)
            notificationText.text = ""; 
    }
}