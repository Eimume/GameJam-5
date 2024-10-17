using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public bool isMoveBackwardTile = false; 
    public int moveBackwardSteps = 3; 
    
    public int damageAmount = 10;

    public bool isDamageTile = false; 
    public bool isShopTile = false; 
    public bool isBattleTile = false;
    public bool isBoss = false;
    //private bool isBossTileActive = true;
    private bool isBattleTileActive = true; // Tracks if the battle tile is active

    public void DeactivateBattleTile()
    {
        isBattleTileActive = false;
    }

    // Method to check if the battle tile is active
    public bool IsBattleTileActive()
    {
        return isBattleTile && isBattleTileActive;
    }

    public bool IsBossTile()
    {
        return isBoss;
    }

    /*public void DeactivateBossTile()
    {
        isBossTileActive = false;
    }

    // Method to check if the battle tile is active
    public bool IsBossTileActive()
    {
        return isBoss && isBossTileActive;
    }*/
}
