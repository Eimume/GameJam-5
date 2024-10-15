using UnityEngine;
using UnityEngine.UI;  // Import UI library

public class PlayerSide : MonoBehaviour
{
    public int health = 100;           // Player's health
    public int currentHp;
    public int normalAttackDamage = 10;  // Damage for a normal attack
    public int specialAttackDamage = 20; // Damage for a special attack

    public ItemType currentItem;  // ไอเท็มที่ผู้เล่นกำลังจะใช้ (เช่น Potion)
    public Text playerHPText;  // อ้างอิงถึง Text UI สำหรับแสดง HP ของผู้เล่น
    public GameObject potionUI;  // หน้าต่าง UI ของ Potion
    public PvPManager pvpManager;
    
    public ActionType selectedAction;  // The action selected by the player

    private void Start()
    {
        currentHp = health;
        UpdatePlayerHPUI();  // Initial update of the Player HP UI
        potionUI.SetActive(false);  // ปิด UI ของ Potion เมื่อเริ่มเกม
    }

    // Update the player's HP in the UI
    private void UpdatePlayerHPUI()
    {
        if (playerHPText != null)
        {
            playerHPText.text = "Player HP: " + currentHp.ToString();
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
        Debug.Log("Healing amount: " + healAmount);
        Debug.Log("Player current HP before healing: " + currentHp);

        currentHp += healAmount;  // เพิ่ม HP ตามค่าที่ฟื้นฟู

        // ตรวจสอบว่า HP ไม่เกินค่าพลังชีวิตสูงสุด
        if (currentHp > health)
        {
            currentHp = health;  // หากเกินค่าพลังชีวิตสูงสุด ให้ตั้งเป็นค่า max
        }

        Debug.Log("Player Health after healing: " + currentHp);
        UpdatePlayerHPUI();  // อัปเดตค่า HP ใน UI หลังจากฟื้นฟู
        pvpManager.UpdateHPUI();
    }

    /*
    public void UseItemFromInventory(ItemType item)
    {

        if (item != null && currentHp < health)
        {
            Heal(item.BuffEffect);  // เรียกใช้ฟังก์ชัน Heal แทนการเพิ่ม HP ตรง ๆ
        }
        else
        {
            Debug.Log("Item is null or player health is already full.");
        }
    }

    public void OpenPotionUI()
    {
        potionUI.SetActive(true);  // เปิดหน้าต่าง UI ของ Potion
    }

    // ฟังก์ชันสำหรับปิด UI ของ Potion โดยไม่ใช้
    public void NoUsePotion()
    {
        potionUI.SetActive(false);  // ปิดหน้าต่าง UI ของ Potion โดยไม่ทำอะไร
    }*/
}
