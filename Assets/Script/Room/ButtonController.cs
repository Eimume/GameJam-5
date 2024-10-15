using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject inventoryButton;
    public GameObject actionButton;

    public GameObject exitButton;

    public GameObject[] actionOptions;
    public GameObject[] inventoryOptions;

    public PlayerSide playerSide;

    private void Start()
    {
        // Start by showing only Inventory and Action buttons, hide the rest
        inventoryButton.SetActive(true);
        actionButton.SetActive(true);
        exitButton.SetActive(false);
   

        foreach (GameObject button in inventoryOptions)
        {
            button.SetActive(false);
        }

        foreach (GameObject button in actionOptions)
        {
            button.SetActive(false);
        }
    }

    // Called when the Inventory button is clicked
    public void OnInventoryButtonClick()
    {
        // Show the Inventory UI, hide other main buttons
        inventoryButton.SetActive(false);
        actionButton.SetActive(false);

        foreach (GameObject button in inventoryOptions)
        {
            button.SetActive(true);
        }

        exitButton.SetActive(true);
    }

    // Called when the Action button is clicked
    public void OnActionButtonClick()
    {
        // Show the Action-specific buttons, hide the main buttons
        inventoryButton.SetActive(false);
        actionButton.SetActive(false);

        foreach (GameObject button in actionOptions)
        {
            button.SetActive(true);
        }

        // Show the Exit button
        exitButton.SetActive(true);
    }

    // Called when the Exit button is clicked
    public void OnExitButtonClick()
    {
        foreach (GameObject button in actionOptions)
        {
            button.SetActive(false);
        }

        foreach (GameObject button in inventoryOptions)
        {
            button.SetActive(false);
        }

        // Show the main buttons again
        inventoryButton.SetActive(true);
        actionButton.SetActive(true);
        exitButton.SetActive(false);
    }
    
    public void OnHealingPotionClick(ItemType item)
    {
        Debug.Log("Healing Potion selected: " + item.itemName);
        playerSide.Heal(item.BuffEffect);  // เรียกฟังก์ชัน Heal ใน PlayerSide โดยส่งค่าการฟื้นฟู
        OnExitButtonClick();  // ปิดเมนูหลังจากใช้ไอเท็ม
    }
}
