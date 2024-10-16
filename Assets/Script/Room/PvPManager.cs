using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PvPManager : MonoBehaviour
{
    public PlayerSide player;
    public EnemySide enemy;

    public Text playerHPText;  // Text UI สำหรับแสดง HP ของผู้เล่น
    public Text enemyHPText;   // Text UI สำหรับแสดง HP ของศัตรู

    public Text whoStartsText;  // UI Text to show who starts
    public GameObject wheelUI;  // The UI element for the wheel that will "spin"

    public ActionType[] possibleAttackActions; // List of possible actions for the enemy to choose from
    public ActionType[] possibleBlockActions;   // Possible blocks (normal, special)
    
    private bool isPlayerTurn = true; // Start with the player's turn
    private bool waitingForPlayerBlock = false;  // Flag to know when we are waiting for player's block


    public void Start()
    {
        UpdateHPUI();  // อัปเดต UI เมื่อเกมเริ่มต้น
        StartCoroutine(SpinToDecideWhoStarts());  // Spin to decide who starts first
        //StartPlayerTurn();
    }

    public void UpdateHPUI()
    {
        //playerHPText.text = "Player HP: " + player.currentHp + " / " + player.health;
        //enemyHPText.text = "Enemy HP: " + enemy.currentHp + " / " + enemy.health;
        player.UpdatePlayerHPUI();
        enemy.UpdateEnemyHPUI();
    }

    IEnumerator SpinToDecideWhoStarts()
    {
        // Activate the wheel UI
        wheelUI.SetActive(true);
        // Simulate the wheel spinning for 2 seconds
        float spinTime = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < spinTime)
        {
            // This could be some animation or text that changes quickly
            whoStartsText.text = (Random.Range(0, 2) == 0) ? "Player" : "Enemy";
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Randomly decide who will start
        bool playerStarts = Random.Range(0, 2) == 0;

        // Display who starts and wait for a moment
        if (playerStarts)
        {
            whoStartsText.text = "Player starts!";
            yield return new WaitForSeconds(1f);
            wheelUI.SetActive(false);  // Hide the wheel
            StartPlayerTurn();  // Start the player's turn
        }
        else
        {
            whoStartsText.text = "Enemy starts!";
            yield return new WaitForSeconds(1f);
            wheelUI.SetActive(false);  // Hide the wheel
            StartEnemyTurn();  // Start the enemy's turn
        }
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
        Debug.Log("It's the player's turn. Choose an action (attack or block).");
        whoStartsText.text = "Player's Turn!";  // Show this in the UI as well
    }

    public void PlayerSelectAction(ActionType action)
    {
        if (isPlayerTurn && !waitingForPlayerBlock)
        {
            player.SelectAction(action);
            Debug.Log("Player selected: " + action.actionName);

            if (action.isAttack)
            {
                enemy.ChooseBlock(possibleBlockActions);
                //Debug.Log("Enemy will block player’s attack.");
            }
            else
            {
                enemy.ChooseAttack(possibleAttackActions);
                //Debug.Log("Enemy will attack while player blocks.");
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

    public void StartEnemyTurn()
    {
        isPlayerTurn = false;
        Debug.Log("It's the enemy's turn. Player must choose a block.");
        whoStartsText.text = "Enemy's Turn!";  // Show this in the UI as well
        enemy.ChooseAttack(possibleAttackActions);
        Debug.Log("Enemy selected: " + enemy.selectedAction.actionName);
        waitingForPlayerBlock = true;
        //Debug.Log("Waiting for player to select a block action...");
        
    }

   public void ResolveTurn()
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

        UpdateHPUI();
    }
}
