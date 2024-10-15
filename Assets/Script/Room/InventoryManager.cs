using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List<ItemType> items = new List<ItemType>();

    // Reference to the UI where the items are displayed (e.g., GridLayoutGroup)
    public Transform inventoryPanel;

    // Prefab for the item slot UI (a button or image to represent the item)
    public GameObject itemSlotPrefab;

    // Reference to the player (to use items)
    public PlayerSide player;

    // Add an item to the inventory
    public void AddItem(ItemType newItem)
    {
        items.Add(newItem);
        UpdateInventoryUI();
    }

    // Update the Inventory UI to display the current items
    private void UpdateInventoryUI()
    {
        // Clear the current UI
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Create a new UI slot for each item in the inventory
        foreach (ItemType item in items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryPanel);

            // Assign the item's icon to the UI slot
            itemSlot.GetComponent<Image>().sprite = item.itemIcon;

            // Add a button to use the item when clicked
            Button itemButton = itemSlot.GetComponent<Button>();
            itemButton.onClick.AddListener(() => UseItem(item));  // Use the item when the button is clicked
        }
    }

    // Function to use the selected item (e.g., potion)
    private void UseItem(ItemType item)
    {
        // Check if it's a healing item like a potion
        if (item != null && item.BuffEffect > 0)
        {
            //player.UseItemFromInventory(item);  // Call PlayerSide to use the item
            items.Remove(item);  // Remove the item from the inventory after use
            UpdateInventoryUI();  // Refresh the inventory UI after the item is used
        }
    }
}
