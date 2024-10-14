using UnityEngine;

public class RockPaperScissorsGame : MonoBehaviour
{
    public RockPaperScissorsEntity playerEntity; // Reference to the Player entity
    public RockPaperScissorsEntity enemyEntity;  // Reference to the Enemy entity

    // Function to start the game when the player makes a choice
    public void PlayerMakesChoice(RockPaperScissor playerChoice)
    {
        playerEntity.SetChoice(playerChoice); // Player chooses
        enemyEntity.RandomizeChoice();        // Enemy chooses at the same time
        DetermineWinner();
    }

    // Function to determine the winner based on the choices
    public void DetermineWinner()
    {
        if (playerEntity.choice == enemyEntity.choice)
        {
            Debug.Log("It's a tie!");
            return;
        }

        // Check if the player's choice beats the enemy's choice
        foreach (RockPaperScissor beatenChoice in playerEntity.choice.beats)
        {
            if (beatenChoice == enemyEntity.choice)
            {
                Debug.Log("Player wins! " + playerEntity.choice.choiceName + " beats " + enemyEntity.choice.choiceName);
                return;
            }
        }

        // If the player didn't win, the enemy wins
        Debug.Log("Enemy wins! " + enemyEntity.choice.choiceName + " beats " + playerEntity.choice.choiceName);
    }
}
