using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int HPplayer = 100;  // HP ของผู้เล่น
    public Text Hp;             // อ้างอิงถึง Text UI สำหรับแสดง HP ของผู้เล่น
    public GameObject potionUI; // หน้าต่าง UI ของ Potion

    public ItemType currentItem;    // อ้างอิงถึง ScriptableObject ของไอเท็มที่กำลังใช้งาน (เช่น Potion)

    void Start() 
    {
        potionUI.SetActive(false);  // ปิด UI ของ Potion เมื่อเริ่มเกม
        UpdatePlayerHPUI();         // อัปเดตค่า HP ใน UI
    }

    void Update()
    {
        Hp.text = $"HP: {HPplayer}";  // แสดงค่า HP บน UI

        if (HPplayer > 100)  // ถ้า HP มากกว่า 100 จะถูกตั้งค่าให้เป็น 100
        {
            HPplayer = 100;
        }
    }

    // ฟังก์ชันสำหรับใช้ไอเท็ม
    public void UseItem()
    {
        if (HPplayer < 100 && currentItem != null)
        {
            HPplayer += currentItem.BuffEffect;  // เพิ่ม HP จากค่าของ BuffEffect ของไอเท็มที่ใช้งาน
            if (HPplayer > 100)
            {
                HPplayer = 100;  // ไม่ให้ HP เกิน 100
            }

            Debug.Log($"Used {currentItem.itemName}, increased HP by {currentItem.BuffEffect}.");
            potionUI.SetActive(false);  // ปิด UI ของ Potion หลังใช้งาน
        }
    }

    // ฟังก์ชันสำหรับเปิด UI ของ Potion
    public void OpenPotionUI()
    {
        potionUI.SetActive(true);  // เปิด UI ของ Potion
    }

    // ฟังก์ชันสำหรับอัปเดตค่า HP ใน UI
    private void UpdatePlayerHPUI()
    {
        Hp.text = $"HP: {HPplayer}";  // อัปเดตข้อความใน UI
    }
}
