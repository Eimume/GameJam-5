using UnityEngine;
using UnityEngine.UI;  // Import UI library
using UnityEngine.SceneManagement;

public class PlayerSide : MonoBehaviour
{

    public Text potionCountText;  // Text สำหรับแสดงจำนวน Potion
    public Text playerHPText;  // อ้างอิงถึง Text UI สำหรับแสดง HP ของผู้เล่น
    public Slider hpSlider;
    //public GameObject potionUI;  // หน้าต่าง UI ของ Potion
    public GameObject lostUI;
    public GameObject replayButton;
    public GameObject exitButton;

    public PvPManager pvpManager;
    public ActionType selectedAction;  // The action selected by the player

    //public bool isdying = false;

    public void Start()
    {
        //isdying = false;
        //currentHp = health;
        UpdatePlayerHPUI();  // Initial update of the Player HP UI
        UpdatePotionCountUI();
        lostUI.SetActive(false);
        replayButton.SetActive(false);
        exitButton.SetActive(false);
        //potionUI.SetActive(false);  // ปิด UI ของ Potion เมื่อเริ่มเกม
    }

    // Update the player's HP in the UI
    public void UpdatePlayerHPUI()
    {
        if (playerHPText != null)
        {
            playerHPText.text = PlayerData.instance.currentHealth + " / " + PlayerData.instance.maxHealth;
        }

        if (hpSlider != null)
        {
            hpSlider.maxValue = PlayerData.instance.maxHealth;
            hpSlider.value = PlayerData.instance.currentHealth;
        }
        
    }

    private void UpdatePotionCountUI()
    {
        if (potionCountText != null)
        {
            potionCountText.text = "Potions: " + PlayerData.instance.currentPotionCount;
        }
    }

    public void SelectAction(ActionType action)
    {
        selectedAction = action;
        Debug.Log("Player selected: " + selectedAction.actionName);
    }

    public void TakeDamage(int damage)
    {
        PlayerData.instance.TakeDamage(damage);
        /*currentHp -= damage;
        if (currentHp < 0) currentHp = 0;*/
        Debug.Log("Player takes " + damage + " damage. Health: " + PlayerData.instance.currentHealth);
        UpdatePlayerHPUI();
        pvpManager.UpdateHPUI();  // Update the UI in PvPManager

        if (PlayerData.instance.currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (lostUI != null)
        {
            lostUI.SetActive(true);
            Debug.Log("Player has lost. Showing Lost UI.");
            if (replayButton != null)
            {
                replayButton.SetActive(true); // Show the Replay Button
                exitButton.SetActive(true);
                //Debug.Log("Player has lost. Showing Lost UI and Replay Button.");
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (PlayerData.instance.currentPotionCount > 0 && PlayerData.instance.currentHealth < PlayerData.instance.maxHealth)
        {
            PlayerData.instance.Heal(healAmount);
            PlayerData.instance.currentPotionCount--;

           // potionCount--;  // ลดจำนวน Potion ลง
            Debug.Log("Player healed by " + healAmount + ". Health: " + PlayerData.instance.currentHealth);
            //Debug.Log("Potions left: " + potionCount);

            //Debug.Log("Player Health after healing: " + currentHp);
            UpdatePlayerHPUI();  // อัปเดตค่า HP ใน UI หลังจากฟื้นฟู
            UpdatePotionCountUI();
            pvpManager.UpdateHPUI();

            if (PlayerData.instance.currentPotionCount <= 0)
            {
                //potionUI.SetActive(false);
                Debug.Log("No potions left!");
            }
         }
        else
        {
            Debug.Log("No potions left or HP is full. Cannot use a potion.");
        }
    }

    public void OnReplayButtonClick()
    {
        // Reset the player state in PlayerData
        PlayerData.instance.ResetPlayerState();
        // Load the "Map" scene
        SceneManager.LoadScene("Map");
    }

    public void OnExitButtonClick()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("Menu");
    }
}
