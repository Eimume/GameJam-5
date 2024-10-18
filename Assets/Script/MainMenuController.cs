using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject creditUI;
    public GameObject exitButton;
    public Text TextNames;
    
    private void Start()
    {
        creditUI.SetActive(false);
        exitButton.SetActive(false);
        TextNames.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Map"); // Load the Map scene when the player clicks start
    }

    // Called when the exit button is clicked
    public void ExitGame()
    {
        Application.Quit(); // Quit the application
        Debug.Log("Game is exiting...");
    }

    public void Showcredit()
    {
        creditUI.SetActive(true);
        exitButton.SetActive(true);
        TextNames.gameObject.SetActive(true);
    }

    public void Closecredit()
    {
        creditUI.SetActive(false);
        exitButton.SetActive(false);
        TextNames.gameObject.SetActive(false);
    }
}
