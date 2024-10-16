using UnityEngine;
using UnityEngine.UI;  // Import UI library

public class PlayerSide : MonoBehaviour
{

    public Text potionCountText;  // Text สำหรับแสดงจำนวน Potion
    public Text playerHPText;  // อ้างอิงถึง Text UI สำหรับแสดง HP ของผู้เล่น
    //public GameObject potionUI;  // หน้าต่าง UI ของ Potion
    public GameObject lostUI;

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
        //potionUI.SetActive(false);  // ปิด UI ของ Potion เมื่อเริ่มเกม
    }

    // Update the player's HP in the UI
    public void UpdatePlayerHPUI()
    {
        if (playerHPText != null)
        {
            playerHPText.text = "Player HP: " + PlayerData.instance.currentHealth + " / " + PlayerData.instance.maxHealth;
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
}
