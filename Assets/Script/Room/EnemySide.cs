using UnityEngine;
using UnityEngine.UI;

public class EnemySide : MonoBehaviour
{
    public ActionType selectedAction;  // The action selected by the enemy
    public int health = 100;           // Enemy's health
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

     public Text enemyHPText;  // Reference to Enemy HP UI Text

    private void Start()
    {
        UpdateEnemyHPUI();  // Initial update of the Enemy HP UI
    }

    // Update the enemy's HP in the UI
    private void UpdateEnemyHPUI()
    {
        enemyHPText.text = "Enemy HP: " + health.ToString();
    }

    public void ChooseBlock(ActionType[] blockActions)
    {
        selectedAction = blockActions[Random.Range(0, blockActions.Length)];
        Debug.Log("Enemy selected block: " + selectedAction.actionName);
    }

    // Enemy chooses an attack if the player is blocking
    public void ChooseAttack(ActionType[] attackActions)
    {
        selectedAction = attackActions[Random.Range(0, attackActions.Length)];
        Debug.Log("Enemy selected attack: " + selectedAction.actionName);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        Debug.Log("Enemy takes " + damage + " damage. Health: " + health);
        UpdateEnemyHPUI();
    }
}
