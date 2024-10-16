using UnityEngine;
using UnityEngine.UI;  // Import UI library

public class PlayerSide : MonoBehaviour
{
    public int health = 100;           // Player's health
    public int currentHp;
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

    public ItemType currentItem;  // ไอเท็มที่ผู้เล่นกำลังจะใช้ (เช่น Potion)
    public int potionCount = 3;  // จำนวน Potion ที่ผู้เล่นมี
    public Text potionCountText;  // Text สำหรับแสดงจำนวน Potion
    public Text playerHPText;  // อ้างอิงถึง Text UI สำหรับแสดง HP ของผู้เล่น
    //public GameObject potionUI;  // หน้าต่าง UI ของ Potion

    public PvPManager pvpManager;
    
    public ActionType selectedAction;  // The action selected by the player

    public void Start()
    {
        currentHp = health;
        UpdatePlayerHPUI();  // Initial update of the Player HP UI
        UpdatePotionCountUI();
        //potionUI.SetActive(false);  // ปิด UI ของ Potion เมื่อเริ่มเกม
    }

    // Update the player's HP in the UI
    public void UpdatePlayerHPUI()
    {
        if (playerHPText != null)
        {
            playerHPText.text = "Player HP: " + currentHp.ToString() +  " / " + health.ToString();
        }
    }

    private void UpdatePotionCountUI()
    {
        if (potionCountText != null)
        {
            potionCountText.text = "Potions: " + potionCount.ToString();
        }
    }

    public void SelectAction(ActionType action)
    {
        selectedAction = action;
        Debug.Log("Player selected: " + selectedAction.actionName);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        Debug.Log("Player takes " + damage + " damage. Health: " + currentHp);
        UpdatePlayerHPUI();
        pvpManager.UpdateHPUI();  // Update the UI in PvPManager
    }

    public void Heal(int healAmount)
    {
        if (potionCount > 0 && currentHp < health)
        {
            Debug.Log("Healing amount: " + healAmount);
            Debug.Log("Player current HP before healing: " + currentHp);

            currentHp += healAmount;  // เพิ่ม HP ตามค่าที่ฟื้นฟู
            if (currentHp > health)
            {
                currentHp = health;  // หากเกินค่าพลังชีวิตสูงสุด ให้ตั้งเป็นค่า max
            }

            potionCount--;  // ลดจำนวน Potion ลง
            Debug.Log("Player healed by " + healAmount + ". Health: " + currentHp);
            Debug.Log("Potions left: " + potionCount);

            Debug.Log("Player Health after healing: " + currentHp);
            UpdatePlayerHPUI();  // อัปเดตค่า HP ใน UI หลังจากฟื้นฟู
            UpdatePotionCountUI();
            pvpManager.UpdateHPUI();

            if (potionCount <= 0)
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
    /*public void OpenPotionUI()
    {
        if (potionCount > 0)
        {
            potionUI.SetActive(true);
        }
        else
        {
            Debug.Log("No potions available to use.");
        }
    }

    public void NoUsePotion()
    {
        potionUI.SetActive(false);
    }*/
}
