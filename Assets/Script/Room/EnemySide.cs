using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySide : MonoBehaviour
{
    public ActionType selectedAction;  // The action selected by the enemy
    public int health = 100;           // Enemy's health
    public int currentHp;
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

    public Text enemyHPText;  // Reference to Enemy HP UI Text
    public Slider hpSlider;
    //public GameObject battleResultPanel;
    public GameObject WinUI;
    public Text battleResultText; // Reference to the text for displaying battle results
    public GameObject exitButton;

    public bool isBoss = false; // Flag to identify if this enemy is a boss


    public void Start()
    {
        currentHp = health;
        UpdateEnemyHPUI();  // Initial update of the Enemy HP UI
        WinUI.SetActive(false);
        exitButton.SetActive(false);
        battleResultText.gameObject.SetActive(false); // Initially hide the result text

        /*if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick); // Add listener for the exit button
        }*/
    }

    // Update the enemy's HP in the UI
    public void UpdateEnemyHPUI()
    {
        enemyHPText.text = currentHp.ToString() + " / " + health.ToString();

        if (hpSlider != null)
        {
            hpSlider.maxValue = health;
            hpSlider.value = currentHp;
        }
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
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        Debug.Log("Enemy takes " + damage + " damage. Health: " + currentHp);
        UpdateEnemyHPUI();

        if (currentHp <= 0)
        {
            if (isBoss)
            {
                StartCoroutine(HandleBossDefeat());
            }
            else
            {
                StartCoroutine(HandleNormalEnemyDefeat());
            }
        }
    }

    private IEnumerator HandleNormalEnemyDefeat()
    {
        WinUI.SetActive(true);
        battleResultText.gameObject.SetActive(true);
        battleResultText.text = "Enemy defeated!";
        yield return new WaitForSeconds(3f);

        // Load the overworld scene
        SceneController.instance.LoadOverworldScene();
    }

    private IEnumerator HandleBossDefeat()
    {
        WinUI.SetActive(true);
        battleResultText.gameObject.SetActive(true);
        battleResultText.text = "You defeated the boss!";
        exitButton.SetActive(true);
        yield return null;
        // Mark the boss battle as won before loading the map scene
        /*PlayerData.instance.MarkBossBattleAsWon();

        // Load the overworld scene
        SceneController.instance.LoadOverworldScene();*/
    }
    public void OnExitButtonClick()
    {
        SceneController.instance.OnExitButtonClick();
    }
}
