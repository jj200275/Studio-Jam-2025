using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class directorScript : MonoBehaviour
{
    public int currentLevel;

    public List<GameObject> itemDatabase = new List<GameObject>();
    public List<GameObject> lvl1ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl1FishSpawns = new List<GameObject>();
    public List<GameObject> lvl2ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl2FishSpawns = new List<GameObject>();

    // Create ItemSpawn/FishSpawn list per level, then assign the empty gameObjects that mark where potential spawns can happen into the corresponding lists

    private void Start()
    {
        switch (currentLevel)
        {
            case 1:
                spawnItems(lvl1ItemSpawns);
                break;
            case 2:
                spawnItems(lvl2ItemSpawns);
                break;
        }
    }

    public void spawnItems(List<GameObject> items)
    {
        List<GameObject> itemsToSpawn = new List<GameObject>(); // List of items that can be spawned in this level
        List<GameObject> temp = new List<GameObject>(); // temp list to keep track of what items have been added, to prevent duplicate additions
        itemDatabase.ForEach(item =>
        {
            temp.Add(item);
        });

        int guaranteedIndex = 0;
        switch (currentLevel)       // Adds guaranteed items per level to the list of items to be spawned, then removes it from the selection
        {
            case 1:
                guaranteedIndex = 2; // Lantern
                break;
            case 2:
                guaranteedIndex = 5; // Flippers
                break;
            case 3:
                guaranteedIndex = 8; // "Medicine"
                break;
            case 4:
                guaranteedIndex = 11; // Armor
                break;
            case 5:
                guaranteedIndex = 12; // Golden Oxygen Tank
                break;
        }
        itemsToSpawn.Add(temp[guaranteedIndex]);
        temp.RemoveAt(guaranteedIndex);

        for (int i = 0; i < items.Count - 1; i++)   // Populate list of items to spawn in the level
        {
            int j = Random.Range(0, temp.Count);
            itemsToSpawn.Add(temp[j]);
            temp.RemoveAt(j);
        }

        foreach (GameObject itemPos in items)   // Spawn items into game world
        {
            int i = Random.Range(0, itemsToSpawn.Count);
            Instantiate(itemsToSpawn[i], itemPos.transform);
            itemsToSpawn.RemoveAt(i);
        }
    }
}
