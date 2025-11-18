using UnityEngine;

// This script is to be assigned to each item prefab
public class ItemData : MonoBehaviour
{
     // assign this in the Inspector for each item prefab
    public Sprite itemIcon;   // for inventory slot UI
    public Sprite achievementIcon;  // e.g. "Defense +3" sprite - for achievement UI at end of scene/level

    [Header("Item Stat Bonuses")]
    public int visionBonus = 0;
    public int speedBonus = 0;
    public int staminaBonus = 0;
    public int defenseBonus = 0;
    public int breathBonus = 0;
}
