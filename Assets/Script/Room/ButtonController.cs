using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject inventoryButton;
    public GameObject actionButton;

    // Exit button, initially hidden
    public GameObject exitButton;

    // Action-specific buttons (e.g., Attack, Defend, etc.)
    public GameObject[] actionOptions;

    // Reference to the Inventory UI (for storing items)
    public GameObject inventoryUI;

    private void Start()
    {
        // Start by showing only Inventory and Action buttons, hide the rest
        inventoryButton.SetActive(true);
        actionButton.SetActive(true);

        // Hide Exit, Inventory UI, and Action options at the start
        exitButton.SetActive(false);
        inventoryUI.SetActive(false);

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
        inventoryUI.SetActive(true);

        // Show the Exit button
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
        // Hide all secondary UI (Inventory and Action options)
        inventoryUI.SetActive(false);
        foreach (GameObject button in actionOptions)
        {
            button.SetActive(false);
        }

        // Show the main buttons again
        inventoryButton.SetActive(true);
        actionButton.SetActive(true);

        // Hide the Exit button
        exitButton.SetActive(false);
    }
}
