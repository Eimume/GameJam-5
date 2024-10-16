using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
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
}
