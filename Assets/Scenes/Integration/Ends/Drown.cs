using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Needed for coroutines

/// <summary>
/// This script now acts as a proper cutscene manager.
/// It fades in the canvas, waits, and then loads the scene
/// that 'directorScript' told it to return to.
/// It also now includes a public function for a button.
/// </summary>
public class Drown : MonoBehaviour
{
    [Header("Canvas Group")]
    public CanvasGroup deathCanvas;

    [Header("Cutscene Timing")]
    public float timeToFade = 1.0f;
    public float timeToWaitAfterFade = 2.0f; // How long to show the canvas

    void Start()
    {
        if (deathCanvas == null)
        {
            Debug.LogError("Drown.cs: deathCanvas is not assigned!");
            return;
        }

        Debug.Log("Drown.cs entered");

        // Ensure the canvas is in the correct starting state:
        // Active, but fully transparent.
        deathCanvas.gameObject.SetActive(true); 
        deathCanvas.alpha = 0f;

        // Start the cutscene sequence
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // 1. --- FADE IT IN ---
        float elapsedTime = 0f;
        while (elapsedTime < timeToFade)
        {
            deathCanvas.alpha = Mathf.Lerp(0f, 1f, elapsedTime / timeToFade);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        deathCanvas.alpha = 1f;

        // --- NEW ---
        // After fading in, make the canvas interactable
        // so the player can click the "Try Again" button.
        deathCanvas.interactable = true;
        deathCanvas.blocksRaycasts = true;

        // 2. --- Wait for a few seconds ---
        // (We can keep this or remove it, depending on if
        // you want to *force* the player to wait)
        yield return new WaitForSeconds(timeToWaitAfterFade);

        // 3. --- Load the next scene ---
        // This will now only run if the player doesn't click
        // the "Try Again" button during the wait time.
        if (!string.IsNullOrEmpty(directorScript.sceneToReturnTo))
        {
            SceneManager.LoadScene(directorScript.sceneToReturnTo);
        }
        else
        {
            Debug.LogError("ERROR: sceneToReturnTo was not set by directorScript!");
            SceneManager.LoadScene("Scenes/Integration/Levels"); // Fallback: Load main menu
        }
    }

    /// <summary>
    /// This public function can be linked to a Button's
    /// OnClick() event in the Unity Inspector.
    /// </summary>
    public void GoBack()
    {
        // It's good practice to reset the time scale
        // in case the game was paused.
        Time.timeScale = 1f;

        // Load the main menu (scene index 0), based on
        // your script's existing fallback logic.
        SceneManager.LoadScene("Scenes/Integration/Levels");
    }
}