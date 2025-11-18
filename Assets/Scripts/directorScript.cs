using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class directorScript : MonoBehaviour
{
    public static int playerVision;
    public static int playerSpeed;
    public static int playerStamina;
    public static int playerDefense;
    public static int playerTotalBreath;

    public static int currentLevel;
    public static string sceneToReturnTo;

    public GameObject fishPrefab;

    public List<GameObject> itemDatabase = new List<GameObject>();
    public List<GameObject> lvl1ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl1FishSpawns = new List<GameObject>();
    public List<GameObject> lvl2ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl2FishSpawns = new List<GameObject>();
    public List<GameObject> lvl3ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl3FishSpawns = new List<GameObject>();
    public List<GameObject> lvl4ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl4FishSpawns = new List<GameObject>();
    public List<GameObject> lvl5ItemSpawns = new List<GameObject>();
    public List<GameObject> lvl5FishSpawns = new List<GameObject>();

    public GameObject level1tiles;
    public GameObject level2tiles;
    public GameObject level3tiles;
    public GameObject level4tiles;
    public GameObject level5tiles;

    // Create ItemSpawn/FishSpawn list per level, then assign the empty gameObjects that mark where potential spawns can happen into the corresponding lists

    private void Start()
    {
        if(currentLevel <= 0) currentLevel = 1;

        level1tiles.SetActive(false);
        level2tiles.SetActive(false);
        level3tiles.SetActive(false);
        level4tiles.SetActive(false);
        level5tiles.SetActive(false);

        switch (currentLevel)
        {
            case 1:
                spawnItems(lvl1ItemSpawns);
                spawnFish(lvl1FishSpawns);
                level1tiles.SetActive(true);
                break;
            case 2:
                spawnItems(lvl2ItemSpawns);
                spawnFish(lvl2FishSpawns);
                level2tiles.SetActive(true);
                break;
            case 3:
                spawnItems(lvl3ItemSpawns);
                spawnFish(lvl3FishSpawns);
                level3tiles.SetActive(true);
                break;
            case 4:
                spawnItems(lvl4ItemSpawns);
                spawnFish(lvl4FishSpawns);
                level4tiles.SetActive(true);
                break;
            case 5:
                spawnItems(lvl5ItemSpawns);
                spawnFish(lvl5FishSpawns);
                level5tiles.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(playerVision + "\n" + playerSpeed + "\n" + playerStamina + "\n" + playerDefense + "\n" + playerTotalBreath + "\n");
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
    public void spawnFish(List<GameObject> fish)
    {
        foreach (GameObject fishPos in fish)
        {
            Instantiate(fishPrefab, fishPos.transform);
        }
    }

    /// <summary>
    /// Call this method when the player exits a level (e.g., from a trigger).
    /// </summary>
    public void PlayerExitedLevel()
    {
        // Check if we just finished Level 1
        if (currentLevel == 1)
        {
            currentLevel++;

            sceneToReturnTo = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("Scenes/Integration/Ends/StartDay2");
        }else if (currentLevel == 2)
        {
            currentLevel++;

            sceneToReturnTo = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("Scenes/Integration/Ends/StartDay3");
        }
        else if (currentLevel == 3)
        {
            currentLevel++;

            sceneToReturnTo = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("Scenes/Integration/Ends/StartDay4");
        }
        else if (currentLevel == 4)
        {
            currentLevel++;

            sceneToReturnTo = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("Scenes/Integration/Ends/StartDay5");
        }
        else if (currentLevel == 5)
        {
            currentLevel = 6;
            SceneManager.LoadScene("Scenes/Integration/BossLevel");
        }
        else if (currentLevel == 6)
        {
            SceneManager.LoadScene("Scenes/Integration/Ends/LabSuccessEnd");
        }
        else
        {
            // For any other level, just advance and reload this scene
            currentLevel++;

            // Reloading the scene will trigger your Start() method again,
            // which will then load the next level's content.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
