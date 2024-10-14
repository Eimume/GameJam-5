using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject[] optionButtons;

    public GameObject mainButton;

    public GameObject exitButton;

    private void Start()
    {
        // Set all option buttons to inactive at the start
        foreach (GameObject button in optionButtons)
        {
            button.SetActive(false);  // Make sure all option buttons are hidden initially
        }

        exitButton.SetActive(false);
    }

    public void OnMainButtonClick()
    {
        // Activate the option buttons when the main button is clicked
        foreach (GameObject button in optionButtons)
        {
            button.SetActive(true);  // Make option buttons visible
        }
        exitButton.SetActive(true);

        mainButton.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        // Deactivate all option buttons and exit button
        foreach (GameObject button in optionButtons)
        {
            button.SetActive(false);  // Hide option buttons
        }
        exitButton.SetActive(false); // Hide the exit button

        // Activate the main button again
        mainButton.SetActive(true);
    }
}
