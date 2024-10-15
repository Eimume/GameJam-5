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
        playerHPText.text = "Player HP: " + health.ToString();
    }

    public void SelectAction(ActionType action)
    {
        selectedAction = action;
        //Debug.Log("Player selected: " + selectedAction.actionName);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;  // ป้องกันไม่ให้ HP ต่ำกว่า 0
        Debug.Log("Player takes " + damage + " damage. Health: " + health);
        UpdatePlayerHPUI();
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
    }
    public void UseItemFromInventory(ItemType item)
    {
        /*
        if (item != null && health < 100)
        {
            Debug.Log($"Used {item.itemName}, increased HP by {item.BuffEffect}.");
            health += item.BuffEffect;  // เพิ่ม HP ตาม BuffEffect ของไอเท็ม
            if (health > 100)
            {
                health = 100;  // ไม่ให้ค่า HP เกิน 100
            }
            
            UpdatePlayerHPUI();  // อัปเดต UI หลังจากใช้ไอเท็ม
            potionUI.SetActive(false);  // ปิดหน้าต่าง Potion หลังใช้งาน
        }
        else
        {
            Debug.Log("Item is null or player health is already full.");
        }*/

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
    }
}
