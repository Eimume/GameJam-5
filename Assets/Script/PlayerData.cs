using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int maxHealth = 100;
    public int currentHealth;
    public int potionCount = 3;
    public int normalAttackDamage = 10;
    public int specialAttackDamage = 20;

    public event Action OnHealthChanged;

    private void Awake()
    {
        // Ensure this is the only instance and it persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent this object from being destroyed when changing scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize player data
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        OnHealthChanged?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke();
        Debug.Log("Player healed, current health: " + currentHealth);
    }
}
