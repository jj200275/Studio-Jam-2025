using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenScript : MonoBehaviour
{
    [Header("Oxygen Bubbles")]
    public Image[] bubbles;
    public int currentOxygen;

    [Header("Timer Settings")]
    [Tooltip("Time in seconds before one bubble depletes.")]
    public float timePerBubble = 1f;

    [Header("Game Over Settings")]
    public string gameOverSceneName = "GameOverScene";
    [Tooltip("Assign your fullscreen black fade image from the Canvas")]
    public Image fadeImage; // <-- **ASSIGN THIS IN THE INSPECTOR**
    [Tooltip("How long in seconds the fade-to-black takes")]
    public float fadeDuration = 1.5f;

    
    public bool gameCompleted = false;
    private bool isGameOver = false; // Prevents this from running multiple times

    void Start()
    {
        timePerBubble = 10 + directorScript.playerTotalBreath;

        currentOxygen = bubbles.Length;

        // Start scene with full oxygen
        foreach (var bub in bubbles)
            bub.fillAmount = 1f;

        // Make sure the fade image is transparent at start
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false); // Make sure it's off
            Color tempColor = fadeImage.color;
            tempColor.a = 0f;
            fadeImage.color = tempColor;
        }

        // Start the oxygen depletion timer
        StartCoroutine(OxygenDepletionRoutine());
    }

    // This Coroutine handles the oxygen timer
    IEnumerator OxygenDepletionRoutine()
    {
        while (!gameCompleted)
        {
            // Wait for the specified amount of time
            yield return new WaitForSeconds(timePerBubble);

            if (!gameCompleted && !isGameOver)
            {
                TakeDamage(1);
            }
        }
    }

    public void updateBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].fillAmount = (i < currentOxygen) ? 1f : 0f;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isGameOver) return; // Don't take damage if game is already ending

        currentOxygen = Mathf.Clamp(currentOxygen - amount, 0, bubbles.Length);
        updateBubbles();

        // Check for Game Over condition
        if (currentOxygen <= 0 && !gameCompleted)
        {
            isGameOver = true; // Set flag
            StopAllCoroutines(); // Stop the oxygen timer
            StartCoroutine(FadeToGameOver()); // Start the fade
        }
    }

    /// <summary>
    /// This new Coroutine handles the fade-to-black and scene load.
    /// </summary>
    IEnumerator FadeToGameOver()
    {
        // 1. Turn on the fade image
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image not assigned in OxygenScript! Loading scene immediately.");
            SceneManager.LoadScene(gameOverSceneName);
            yield break; // Stop the coroutine
        }
        
        fadeImage.gameObject.SetActive(true);

        // 2. Loop over the fade duration
        float timer = 0f;
        Color startColor = fadeImage.color; // Is (0, 0, 0, 0)
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Is (0, 0, 0, 1)

        while (timer < fadeDuration)
        {
            // Advance the timer. Use unscaledDeltaTime to work even if game is paused
            timer += Time.unscaledDeltaTime; 
            
            // Calculate the new alpha and set the color
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            
            yield return null; // Wait for the next frame
        }

        // 3. Ensure it's fully black
        fadeImage.color = endColor;

        // 4. Load the Game Over scene
        SceneManager.LoadScene(gameOverSceneName);
    }


    public void WinGame()
    {
        gameCompleted = true;
        StopAllCoroutines();
    }
}