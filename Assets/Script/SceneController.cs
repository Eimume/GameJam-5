using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public DiceRoller diceRoller;
    private Vector3 savedPlayerPosition;
    private int savedTileIndex;
    public GameObject exitButton;
    public GameObject winMessageUI;

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
        // Hide the win message UI initially
        if (winMessageUI != null)
        {
            winMessageUI.SetActive(false);
            exitButton.SetActive(false);
        }
    }

    public void SavePlayerState(Vector3 position, int tileIndex)
    {
        // Save the player's position and tile index
        savedPlayerPosition = position;
        savedTileIndex = tileIndex;
    }

    public void LoadBossScene()
    {
        if (diceRoller != null)
        {
            diceRoller.HideDiceUI();
        }

        StartCoroutine(LoadBoss());
    }
    public void LoadRandomBattleScene()
    {
        if (diceRoller != null)
        {
            diceRoller.HideDiceUI();
        }
        Debug.Log("Load Battle scene");
        string[] battleScenes = { "Monster", "Monster2" };
        string chosenScene = battleScenes[Random.Range(0, battleScenes.Length)];
        StartCoroutine(LoadBattle(chosenScene));
    }

    private IEnumerator LoadBoss()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("PVPScene");
    }

    private IEnumerator LoadBattle(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadOverworldScene()
    {
        SceneManager.LoadScene("map");
        StartCoroutine(RestorePlayerStateAfterSceneLoad());
    }

    private IEnumerator RestorePlayerStateAfterSceneLoad()
    {
        // Wait for the scene to load completely
        yield return new WaitForEndOfFrame();

        sumlong player = FindObjectOfType<sumlong>();
        // Find the player and restore their position and tile index
        if (player != null)
        {
            player.RestorePlayerState(savedPlayerPosition, savedTileIndex);
        }

        if (PlayerData.instance != null && PlayerData.instance.hasWonBossBattle)
        {
            ShowWinMessage();
        }

        // Show the dice UI again if needed
        if (diceRoller != null)
        {
            diceRoller.ShowDiceUI();
        }
    }

    public void ResetAndLoadMapScene()
    {
        // Reset the player's state
        PlayerData.instance.ResetPlayerState();
        // Load the "Map" scene
        SceneManager.LoadScene("map");
    }

    private void ShowWinMessage()
    {
        if (winMessageUI != null)
        {
            winMessageUI.SetActive(true);
            exitButton.SetActive(true);
        }
    }

    public void OnExitButtonClick()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("Menu");
    }
}
