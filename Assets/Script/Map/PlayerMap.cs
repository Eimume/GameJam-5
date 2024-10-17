using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMap : MonoBehaviour
{
    private PlayerData playerData;

    // UI components for displaying HP
    public Text hpText; // For TextMeshPro usage
    public Slider hpSlider; // For Slider usage

    private void Start()
    {
        // Get reference to PlayerData instance
        playerData = PlayerData.instance;

        // Check if PlayerData instance is available
        if (playerData == null)
        {
            Debug.LogError("PlayerData instance not found!");
            return;
        }

        // Initialize UI elements based on current health
        UpdateHPUI();

        // Subscribe to PlayerData's health change event if you want real-time updates
        // This requires adding an event in PlayerData if needed.
    }

    private void Update()
    {
        // Continuously update the HP display based on player health
        UpdateHPUI();
    }

    // Method to update HP in the UI
    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = $"     : {playerData.currentHealth} / {playerData.maxHealth}";
        }

        if (hpSlider != null)
        {
            hpSlider.maxValue = playerData.maxHealth;
            hpSlider.value = playerData.currentHealth;
        }
    }

    private void OnDestroy()
{
    // Unsubscribe from the event to avoid memory leaks
    if (playerData != null)
    {
        playerData.OnHealthChanged -= UpdateHPUI;
    }
}
}
