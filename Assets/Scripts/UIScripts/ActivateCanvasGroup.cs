using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ActivateCanvasGroup : MonoBehaviour
{
    // activates canvas group AND LOADS SCENE
    [Header("Canvas Groups")]
    // public CanvasGroup startDayCanvas;      // set inactive
    // public CanvasGroup playerBarsCanvas;    // set inactive

    public CanvasGroup deathCanvas;
    public CanvasGroup endLevelCanvas;
    public CanvasGroup labSuccessCanvas;  // end of game

    public TMP_Text labSuccessText;

    [Header("Game State Flags")]
    public bool playerDied = false;
    public bool playerReachedEnd = false;
    public bool playerSucceeded = false;
    private bool hasTriggered = false; // prevent multiple triggers

    // NOT in Inspector
    private int sceneIndex = 0; // CHANGE TO WHAT SCENE NEED
    private int score = 0;  // adjust


    private void Update()
    {
        if (!hasTriggered)
        {
            if (playerDied)
            {
                hasTriggered = true;

                // startDayCanvas.gameObject.SetActive(false);
                // playerBarsCanvas.gameObject.SetActive(false);

                deathCanvas.gameObject.SetActive(true);
                StartCoroutine(Fade.fadeCanvas(deathCanvas, 1f, 1f));
                // scene of sceneIndex will load when player click button
            }

            else if (playerReachedEnd)
            {
                hasTriggered = true;

                // startDayCanvas.gameObject.SetActive(false);
                // playerBarsCanvas.gameObject.SetActive(false);

                StartCoroutine(FadeAndLoadScene(endLevelCanvas, sceneIndex));
            }

            else if (playerSucceeded)
            {
                hasTriggered = true;

                // startDayCanvas.gameObject.SetActive(false);
                // playerBarsCanvas.gameObject.SetActive(false);

                labSuccessText.text = $"\"Congratulations! You've saved {score} / 5 axolotls!\"";
            }
        }
    }


        private IEnumerator FadeAndLoadScene(CanvasGroup canvasGroup, int sceneToLoad)
        {
            if (canvasGroup != null)
            {
                canvasGroup.gameObject.SetActive(true);
                yield return StartCoroutine(Fade.fadeCanvas(canvasGroup, 1f, 1f));
            }
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneToLoad);
        }

// -------------------------
//     BUTTON LOAD SCENE
// -------------------------

        public void LoadSceneNow() // for when player clicks button in death route
        {
            SceneManager.LoadScene(sceneIndex); // loads the assigned scene
        }

// -------------------------
//         SETTERS
// -------------------------
        public void SetSceneIndex(int index)
        {
            sceneIndex = index;
        }

        public void SetScore(int newScore)
        {
            score = newScore;
        }
}
