using UnityEngine;

public class PlayerSide : MonoBehaviour
{
    public ActionType selectedAction;  // The action selected by the player
    public int health = 100;           // Player's health
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

    public void SelectAction(ActionType action)
    {
        selectedAction = action;
        //Debug.Log("Player selected: " + selectedAction.actionName);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player takes " + damage + " damage. Health: " + health);
    }
}
