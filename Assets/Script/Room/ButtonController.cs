using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public GameObject inventoryButton;
    public GameObject actionButton;

    public GameObject exitButton;
    //public GameObject LoseUI;
    public GameObject inventoryUI;

    public GameObject[] actionOptions;
    public GameObject[] inventoryOptions;

    public GameObject replayButton; // Replay Button GameObject
    public GameObject lostUI;

    public PlayerSide playerSide;
    public GameObject potionCountTextUI;

    private void Start()
    {
        // Start by showing only Inventory and Action buttons, hide the rest
        inventoryButton.SetActive(true);
        actionButton.SetActive(true);
        exitButton.SetActive(false);
        inventoryUI.SetActive(false);
        //LoseUI.SetActive(false);

        potionCountTextUI.SetActive(false);
        lostUI.SetActive(false); // Hide the Lost UI at the start
        replayButton.SetActive(false); // Hide the replay button at the start

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
        inventoryUI.SetActive(true);
        potionCountTextUI.SetActive(true);

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
        potionCountTextUI.SetActive(false);
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

        potionCountTextUI.SetActive(false);
        inventoryUI.SetActive(false);

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

    public void ShowLostUI()
    {
        if (lostUI != null)
        {
            lostUI.SetActive(true);

            if (replayButton != null)
            {
                replayButton.SetActive(true); // Make sure the replay button is activated
                Debug.Log("Replay Button is now active.");
            }
            else
            {
                Debug.LogWarning("Replay Button reference is missing!");
            }
        }
    }

    public void OnReplayButtonClick()
    {
        SceneController.instance.ResetAndLoadMapScene();
    }
}
