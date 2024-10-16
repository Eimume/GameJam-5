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

    public void Start()
    {
        currentHp = health;
        UpdateEnemyHPUI();  // Initial update of the Enemy HP UI
        WinUI.SetActive(false);
        battleResultText.gameObject.SetActive(false); // Initially hide the result text
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
            StartCoroutine(HandleEnemyDefeat());
        }
    }

    private IEnumerator HandleEnemyDefeat()
    {
        WinUI.SetActive(true);
        // Display "Player won!!"
        battleResultText.gameObject.SetActive(true);
        battleResultText.text = "Player won!!";
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Display "Prize: "
        battleResultText.text = "Prize: 100 gold";
        yield return new WaitForSeconds(3f); // Wait for another 3 seconds

        // Load the overworld scene
        SceneController.instance.LoadOverworldScene();
    }
}
