using UnityEngine;

public class PvPManager : MonoBehaviour
{
    public PlayerSide player;
    public EnemySide enemy;

    public ActionType[] possibleAttackActions; // List of possible actions for the enemy to choose from
    public ActionType[] possibleBlockActions;   // Possible blocks (normal, special)
    
    private bool isPlayerTurn = true; // Start with the player's turn
    private bool waitingForPlayerBlock = false;  // Flag to know when we are waiting for player's block


    public void Start()
    {
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        Debug.Log("It's the player's turn. Choose an action (attack or block).");
    }

    public void PlayerSelectAction(ActionType action)
    {
        if (isPlayerTurn && !waitingForPlayerBlock)
        {
            player.SelectAction(action);
            Debug.Log("Player selected: " + action.actionName);

            // Player chooses an attack
            if (action.isAttack)
            {
                // Enemy must block
                enemy.ChooseBlock(possibleBlockActions);
                Debug.Log("Enemy will block player’s attack.");
            }
            // Player chooses a block
            else
            {
                // Enemy must attack
                enemy.ChooseAttack(possibleAttackActions);
                Debug.Log("Enemy will attack while player blocks.");
            }

            // Resolve the player's turn and switch to enemy's turn
            ResolveTurn();
            isPlayerTurn = false; // Switch turn
            StartEnemyTurn();
        }
        else if (waitingForPlayerBlock) // Player chooses a block during enemy's turn
        {
            player.SelectAction(action);  // Player selects their block action
            Debug.Log("Player selected a block during enemy's turn: " + action.actionName);

            ResolveTurn();  // Now resolve the enemy's attack and player's block
            waitingForPlayerBlock = false;
            isPlayerTurn = true; // Switch back to the player's turn after resolving
            StartPlayerTurn();
        }
    }

    private void StartEnemyTurn()
    {
        // Assume the enemy chooses either an attack or block
        if (!isPlayerTurn)
        {

            Debug.Log("Enemy's turn. Player must choose a block.");

            // Enemy always chooses an attack during its turn
            enemy.ChooseAttack(possibleAttackActions);

            // Now we wait for the player to choose a block action
            waitingForPlayerBlock = true;
            Debug.Log("Waiting for player to select a block action...");
        }
    }

   private void ResolveTurn()
    {
        Debug.Log("Resolving turn...");

        Debug.Log("Player action: " + player.selectedAction.actionName);
        Debug.Log("Enemy action: " + enemy.selectedAction.actionName);

       // Player attack, enemy block
        if (player.selectedAction.isAttack && !enemy.selectedAction.isAttack)
        {
            if (!player.selectedAction.isSpecial && !enemy.selectedAction.isSpecial)
            {
                Debug.Log("Player's normal attack blocked by enemy's normal block. No damage dealt.");
            }
            else if (!player.selectedAction.isSpecial && enemy.selectedAction.isSpecial)
            {
                enemy.TakeDamage(player.normalAttackDamage);
                Debug.Log("Player's normal attack bypasses enemy's special block. Enemy takes damage.");
            }
            else if (player.selectedAction.isSpecial && !enemy.selectedAction.isSpecial)
            {
                enemy.TakeDamage(player.specialAttackDamage);
                Debug.Log("Player's special attack bypasses enemy's normal block. Enemy takes damage.");
            }
            else if (player.selectedAction.isSpecial && enemy.selectedAction.isSpecial)
            {
                Debug.Log("Player's special attack blocked by enemy's special block. No damage dealt.");
            }
        }
        // Player block, enemy attack
        else if (!player.selectedAction.isAttack && enemy.selectedAction.isAttack)
        {
            if (!enemy.selectedAction.isSpecial && !player.selectedAction.isSpecial)
            {
                Debug.Log("Enemy's normal attack blocked by player's normal block. No damage dealt.");
            }
            else if (enemy.selectedAction.isSpecial && !player.selectedAction.isSpecial)
            {
                player.TakeDamage(enemy.specialAttackDamage);
                Debug.Log("Enemy's special attack bypasses player's normal block. Player takes damage.");
            }
            else if (enemy.selectedAction.isSpecial && player.selectedAction.isSpecial)
            {
                Debug.Log("Enemy's special attack blocked by player's special block. No damage dealt.");
            }
            else if (!enemy.selectedAction.isSpecial && player.selectedAction.isSpecial)
            {
                player.TakeDamage(enemy.normalAttackDamage);
                Debug.Log("Enemy's normal attack bypasses player's special block. Player takes damage.");
            }
        }
    }

    /*// Example of running a game turn
    public void RunTurn()
    {
        // Player selects an action (this could come from UI)
        player.SelectAction(possibleActions[0]);  // Assume player selects the first action

        // Enemy randomly selects an action
        enemy.SelectRandomAction(possibleActions);

        // Resolve the turn
        ResolveTurn();
    }*/
}
