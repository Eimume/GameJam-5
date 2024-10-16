using UnityEngine;
using System;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int maxHealth = 100;
    public int currentHealth;
    public int potionCount = 3;
    public int currentPotionCount;
    public int normalAttackDamage = 10;
    public int specialAttackDamage = 20;

    public Vector3 lastPosition; // Store the last position of the player
    public int lastTileIndex; // Store the last tile index of the player

    public int currentTileIndex;
    

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
        ResetPlayerState();

        /*lastPosition = Vector3.zero;
        lastTileIndex = 0;*/
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

    public void SavePlayerPosition(Vector3 position, int tileIndex)
    {
        lastPosition = position;
        lastTileIndex = tileIndex;
        Debug.Log("Position saved: " + position + ", Tile Index: " + tileIndex);
    }
    public void ResetPlayerState()
    {
        // Reset the player state to its initial values
        currentHealth = maxHealth;
        currentPotionCount = potionCount;

        lastPosition = Vector3.zero;
        lastTileIndex = 0;
        // Reset other attributes if needed
    }
}
