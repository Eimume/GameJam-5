using UnityEngine;
using UnityEngine.UI;  // Import UI library

public class PlayerSide : MonoBehaviour
{
    public ActionType selectedAction;  // The action selected by the player
    public int health = 100;           // Player's health
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

    public Text playerHPText;  // Reference to Player HP UI Text

    private void Start()
    {
        UpdatePlayerHPUI();  // Initial update of the Player HP UI
    }

    // Update the player's HP in the UI
    private void UpdatePlayerHPUI()
    {
        playerHPText.text = "Player HP: " + health.ToString();
    }

    public void SelectAction(ActionType action)
    {
        selectedAction = action;
        //Debug.Log("Player selected: " + selectedAction.actionName);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player takes " + damage + " damage. Health: " + health);
        UpdatePlayerHPUI();
    }
}
