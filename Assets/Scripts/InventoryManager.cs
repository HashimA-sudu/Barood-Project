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

    void Awake() { Instance = this; }

    public bool AddItem(InventoryItem item)
    {
        if (item.isWeapon)
        {
            if (weapons.Count >= maxWeapons)
            {
                ShowNotification("Weapon Slots Full!");
                return false;
            }
            weapons.Add(item);
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
        notificationText.text = message;
    }

    void HideNotification() { notificationText.text = ""; }
}