using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ActivateCanvasGroup : MonoBehaviour
{
    // activates canvas group AND LOADS SCENE
    [Header("Canvas Groups")]
    public CanvasGroup deathCanvas;
    public CanvasGroup endLevelCanvas;

    [Header("Game State Flags")]
    public bool playerDied = false;
    public bool playerReachedEnd = false;
    private bool hasTriggered = false; // prevent multiple triggers

    // NOT in Inspector
    private int sceneIndex = 0; // CHANGE TO WHAT SCENE NEED


    private void Update()
    {
        if (!hasTriggered)
        {
            if (playerDied)
            {
                hasTriggered = true;
                deathCanvas.gameObject.SetActive(true);
                StartCoroutine(Fade.fadeCanvas(deathCanvas, 1f, 1f));
                LoadSceneByIndex(sceneIndex);
            }
            else if (playerReachedEnd)
            {
                hasTriggered = true;
                endLevelCanvas.gameObject.SetActive(true);
                StartCoroutine(Fade.fadeCanvas(endLevelCanvas, 1f, 1f));
                LoadSceneByIndex(sceneIndex);
            }
        }
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        // check if index is valid
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
        }
    }
}
