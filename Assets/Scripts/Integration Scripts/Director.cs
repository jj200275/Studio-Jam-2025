using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // --- NEW --- Needed for loading scenes

// --- NEW ---
// Defines the possible outcomes of a level.
// Your 'ActivateCanvasGroup' script can read this to know what to display.
public enum LevelEndState { None, Died, ReachedEnd, Succeeded }

public class Director : MonoBehaviour
{
    // --- NEW --- Singleton Pattern
    public static Director Instance;

    [Header("Game State")]
    public static int currentLevel;
    public LevelEndState lastLevelState = LevelEndState.None; // --- NEW ---
    [SerializeField] private int maxLevel = 5; // --- NEW --- Set to your max level

    [Header("Scene Names")]
    // --- NEW --- Assign these in the Inspector
    [SerializeField] private string mainGameSceneName = "MainGameScene"; 
    [SerializeField] private string endLevelSceneName = "EndLevelScene"; // Your scene with ActivateCanvasGroup
    [SerializeField] private string winSceneName = "WinScene";
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    // [SerializeField] private string loseSceneName = "LoseScene"; // Optional

    [Header("Player Stats")]
    public static int playerVision;
    public static int playerSpeed;
    public static int playerStamina;
    public static int playerDefense;
    public static int playerTotalBreath;


    [Header("Level Setup")]
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


    // --- NEW --- Awake() runs before Start()
    private void Awake()
    {
        // This is the Singleton pattern.
        // It ensures only one 'Director' ever exists
        // and that it persists between scenes.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another one already exists, destroy this new one.
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        if(currentLevel <= 0) currentLevel = 1;

        // Reset the level state every time the game scene loads
        lastLevelState = LevelEndState.None;

        DeactivateAllLevels(); // --- NEW --- Cleaner way to hide all levels

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

    // --- NEW --- Helper function for Start()
    private void DeactivateAllLevels()
    {
        level1tiles.SetActive(false);
        level2tiles.SetActive(false);
        level3tiles.SetActive(false);
        level4tiles.SetActive(false);
        level5tiles.SetActive(false);
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


    // --------------------------------------------------
    // --- NEW --- SCENE MANAGEMENT METHODS ---
    // --------------------------------------------------

    /// <summary>
    /// Call this from your "end level portal" or trigger when the player wins.
    /// </summary>
    public void PlayerFinishedLevel()
    {
        currentLevel++; // Advance to the next level

        if (currentLevel > maxLevel)
        {
            // Player just finished the last level
            lastLevelState = LevelEndState.Succeeded;
            SceneManager.LoadScene(winSceneName); // Or load endLevelSceneName to show a final "You Won!" canvas
        }
        else
        {
            // Player finished a normal level
            lastLevelState = LevelEndState.ReachedEnd;
            SceneManager.LoadScene(endLevelSceneName); // Load the canvas scene
        }
    }

    /// <summary>
    /// Call this from the "Next Level" button on your 'endLevelCanvas'.
    /// </summary>
    public void LoadNextLevel()
    {
        // This reloads the main game scene.
        // Because 'currentLevel' was already incremented, 
        // the Start() method will automatically set up the *new* level.
        SceneManager.LoadScene(mainGameSceneName);
    }

    /// <summary>
    /// Call this from a "Main Menu" button.
    /// </summary>
    public void LoadMainMenu()
    {
        currentLevel = 1; // Reset for a new game
        lastLevelState = LevelEndState.None;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Call this when the player dies.
    /// </summary>
    public void PlayerDied()
    {
        lastLevelState = LevelEndState.Died;
        SceneManager.LoadScene(endLevelSceneName); // Load the canvas scene
    }

    /// <summary>
    // Call this from the "Try Again" button on your 'deathCanvas'.
    /// </summary>
    public void RestartCurrentLevel()
    {
        // No need to change currentLevel, just reload the scene
        lastLevelState = LevelEndState.None;
        SceneManager.LoadScene(mainGameSceneName);
    }
}