using UnityEngine;

// Create an empty GameObject in your scene named "GameManager" or "PlayerStats"
// and attach this script to it.
public class PlayerStats : MonoBehaviour
{
    // --- SET BASE STATS IN INSPECTOR ---
    // These are the player's stats when holding NO item.
    [Header("Base Stats (No Item)")]
    public int baseVision = 0;
    public int baseSpeed = 0;
    public int baseStamina = 0;
    public int baseDefense = 0;
    public int baseTotalBreath = 0;

    // --- CURRENT STATS (Static) ---
    // These are the TOTAL stats (Base + Item Bonus) that other
    // scripts can read from.
    public static int playerVision;
    public static int playerSpeed;
    public static int playerStamina;
    public static int playerDefense;
    public static int playerTotalBreath;

    // Singleton instance to get base stats
    private static PlayerStats instance;

    private void Awake()
    {
        // Set up the singleton instance
        if (instance == null)
        {
            instance = this;
            // Set the stats to their base values when the game starts
            ResetStatsToBaseline();
            // DontDestroyOnLoad(gameObject); // Optional: use if stats need to persist
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Resets all stats to the base values.
    /// Call this when an item is dropped or stolen.
    /// </summary>
    public static void ResetStatsToBaseline()
    {
        if (instance == null) return;

        playerVision = instance.baseVision;
        playerSpeed = instance.baseSpeed;
        playerStamina = instance.baseStamina;
        playerDefense = instance.baseDefense;
        playerTotalBreath = instance.baseTotalBreath;
        
        Debug.Log("Stats Reset to Base: Speed(" + playerSpeed + "), Vision(" + playerVision + ")");
    }

    /// <summary>
    /// Applies an item's bonuses to the base stats.
    /// Call this when an item is picked up.
    /// </summary>
    public static void ApplyItemStats(ItemData item)
    {
        if (item == null) return;

        // Start from a clean slate (base stats)
        ResetStatsToBaseline();

        // Add the item's bonuses
        playerVision += item.visionBonus;
        playerSpeed += item.speedBonus;
        playerStamina += item.staminaBonus;
        playerDefense += item.defenseBonus;
        playerTotalBreath += item.breathBonus;

        Debug.Log("Applied stats for: " + item.name + 
                  " | New Speed: " + playerSpeed + ", New Vision: " + playerVision);
    }
}