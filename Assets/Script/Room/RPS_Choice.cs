using UnityEngine;
using UnityEngine.UI;

public class RPS_Choice : MonoBehaviour
{
    public RockPaperScissor playerChoice;
    public RockPaperScissor enemyChoice;

    public Image playerIconUI; // UI Image for player icon
    public Image enemyIconUI;     // UI Image for AI icon

    public RockPaperScissor[] allChoices;

    public GameObject playerObject; // Reference to the player GameObject
    public GameObject enemyObject;  // Reference to the enemy GameObject

    // Function to set the player's choice (to be called when player makes a choice)
    public void SetPlayerChoice(RockPaperScissor choice)
    {
        playerChoice = choice;
        Debug.Log("Player chose: " + playerChoice.choiceName);
        playerIconUI.sprite = playerChoice.icon;
        SetRandomEnemyChoice(); // Enemy chooses after player
    }

    // Function to set the enemy's random choice
    public void SetRandomEnemyChoice()
    {
        int randomIndex = Random.Range(0, allChoices.Length);
        enemyChoice = allChoices[randomIndex];
        Debug.Log("Enemy chose: " + enemyChoice.choiceName);
        enemyIconUI.sprite = enemyChoice.icon;

        DetermineWinner();
    }

    public void DetermineWinner()
    {
        if (playerChoice == enemyChoice)
        {
            Debug.Log("It's a tie!");
            return;
        }

        foreach (RockPaperScissor beatenChoice in playerChoice.beats)
        {
            if (beatenChoice == enemyChoice)
            {
                Debug.Log("Player wins! " + playerChoice.choiceName + " beats " + enemyChoice.choiceName);
                return;
            }
        }

        Debug.Log("Enemy wins! " + enemyChoice.choiceName + " beats " + playerChoice.choiceName);
    }

}
