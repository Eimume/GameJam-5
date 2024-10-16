using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject BattleMessage;
    public TextMeshProUGUI battleNotificationText;
    public DiceRoller diceRoller; // Reference to the DiceRoller script
    public sumlong player;
    private Vector3 savedPlayerPosition;
    private int savedTileIndex;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist this object across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find the DiceRoller script in the scene
        BattleMessage.SetActive(false);
        if (battleNotificationText != null)
        {
            battleNotificationText.gameObject.SetActive(false); // Hide the notification text initially
        }
    }
    public void SavePlayerState(Vector3 position, int tileIndex)
    {
        // Save the player's position and tile index
        savedPlayerPosition = position;
        savedTileIndex = tileIndex;
    }


    public void LoadBattleScene()
    {
        if (diceRoller != null)
        {
            diceRoller.HideDiceUI();
        }
        // Start the coroutine to display messages before transitioning
        StartCoroutine(ShowBattleMessages());
    }

    private IEnumerator ShowBattleMessages()
    {
        BattleMessage.SetActive(true);
        // Display the first message
        battleNotificationText.text = "You have entered a Battle Tile!";
        battleNotificationText.gameObject.SetActive(true); // Make sure the text is visible
        yield return new WaitForSeconds(2f); // Wait for 3 seconds

        // Display the next message
        battleNotificationText.text = "Prepare to Fight!";
        yield return new WaitForSeconds(3f); // Wait for another 3 seconds

        // Hide the text and load the battle scene
        battleNotificationText.gameObject.SetActive(false);
        SceneManager.LoadScene("PVPScene");
    }


    public void LoadOverworldScene()
    {
        SceneManager.LoadScene("MapTestLoadScene");
        StartCoroutine(RestorePlayerStateAfterSceneLoad());
    }

    private IEnumerator RestorePlayerStateAfterSceneLoad()
    {
        // Wait for the scene to load completely
        yield return new WaitForEndOfFrame();

        // Find the player and restore their position and tile index
        if (player != null)
        {
            player.RestorePlayerState(savedPlayerPosition, savedTileIndex);
        }

        // Show the dice UI again if needed
        if (diceRoller != null)
        {
            diceRoller.ShowDiceUI();
        }
    }
}
