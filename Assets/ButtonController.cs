using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject[] optionButtons;

    // This function is called when the main button is clicked
    public void OnMainButtonClick()
    {
        // Toggle the visibility of the option buttons
        foreach (GameObject button in optionButtons)
        {
            bool isActive = button.activeSelf;
            button.SetActive(!isActive); // Switch between visible and hidden
        }
    }
}
