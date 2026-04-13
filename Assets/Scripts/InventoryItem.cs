using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public bool isWeapon;
    public GameObject worldPrefab; // The 3D model that sits on the ground
}