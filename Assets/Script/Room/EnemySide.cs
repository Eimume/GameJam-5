using UnityEngine;

public class EnemySide : MonoBehaviour
{
    public ActionType selectedAction;  // The action selected by the enemy
    public int health = 100;           // Enemy's health
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

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
        Debug.Log("Enemy takes " + damage + " damage. Health: " + health);
    }
}
