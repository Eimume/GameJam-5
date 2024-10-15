using UnityEngine;
using TMPro; // �������������ҹ TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI healthText; // ��ҧ�ԧ��ѧ UI Text

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // �Ѿഷ UI ������������
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI(); // �Ѿഷ UI �ء���駷���آ�Ҿ����¹�ŧ

        if (currentHealth <= 0)
        {
            // �����ѧ��ѹ����Ѻ�óշ������蹵��
            Debug.Log("Player is dead.");
            // ������ҧ: �Ҩ����Ŵ Scene ���� �����ʴ�˹�Ҩ� Game Over
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

    // �����ѧ��ѹ����Ѻ�������ʹ (��ҵ�ͧ���)
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }
}
