using UnityEngine;

/// <summary>
/// Script for items that can be picked up by the player.
/// Place this on loot prefabs to make them interactive.
/// </summary>
public class Pickup : MonoBehaviour, IInteractable
{
    public string itemName = "Item";
    public InventoryItem inventoryItem;

    void Start()
    {
        // Ensure there's a collider
        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning($"Pickup ({itemName}): No Collider found! Adding a Sphere Collider.");
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = false; // Important: Must NOT be a trigger for raycast detection
        }
        
        // Verify collider is NOT a trigger
        Collider col = GetComponent<Collider>();
        if (col != null && col.isTrigger)
        {
            Debug.LogWarning($"Pickup ({itemName}): Collider is set to trigger! This will break pickup detection. Disabling trigger mode.");
            col.isTrigger = false;
        }
    }

    public string GetInteractLabel()
    {
        return $"Press E to pickup {itemName}";
    }

    public void Interact()
    {
        Debug.Log($"Picking up {itemName}");
        
        if (inventoryItem == null)
        {
            Debug.LogWarning($"Pickup ({itemName}): InventoryItem not assigned!");
            Destroy(gameObject);
            return;
        }
        
        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("Pickup: InventoryManager.Instance not found!");
            Destroy(gameObject);
            return;
        }
        
        // Try to add item to inventory
        if (InventoryManager.Instance.AddItem(inventoryItem))
        {
            Debug.Log($"Successfully added {itemName} to inventory");
            
            // If it's a weapon, unlock/equip it in the WeaponManager
            if (inventoryItem.isWeapon && WeaponManager.Instance != null)
            {
                WeaponManager.Instance.UnlockWeapon(itemName);
            }
            
            // Remove the pickup from the world
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"Failed to add {itemName} to inventory (slots full?)");
        }
    }
}

