using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public InventoryItem itemData;

    public string GetInteractLabel()
    {
        return "Press [E] to pickup " + itemData.itemName;
    }

    public void Interact()
    {
        bool wasPickedUp = InventoryManager.Instance.AddItem(itemData);
        
        if (wasPickedUp)
        {
            Destroy(gameObject); // Remove from the world
        }
    }
}