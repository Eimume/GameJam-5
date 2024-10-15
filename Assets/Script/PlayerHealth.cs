using UnityEngine;
using TMPro; // เพิ่มนี้เพื่อใช้งาน TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI healthText; // อ้างอิงไปยัง UI Text

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // อัพเดท UI เมื่อเริ่มเกม
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI(); // อัพเดท UI ทุกครั้งที่สุขภาพเปลี่ยนแปลง

        if (currentHealth <= 0)
        {
            // เพิ่มฟังก์ชันสำหรับกรณีที่ผู้เล่นตาย
            Debug.Log("Player is dead.");
            // ตัวอย่าง: อาจจะโหลด Scene ใหม่ หรือแสดงหน้าจอ Game Over
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
        else
        {
            Debug.LogWarning("HealthText is not assigned in the PlayerHealth script.");
        }
    }

    // เพิ่มฟังก์ชันสำหรับเพิ่มเลือด (ถ้าต้องการ)
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }
}
