using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StartContinuedScript : MonoBehaviour
{
    public Image blackScreen;        // use this to fade screen
    public TMP_Text dialogueText;
    public Button continueButton;

    private void Start()
    {
        Fade.setAlpha(dialogueText, 0);
        blackScreen.color = Color.black;   // start black
        continueButton.gameObject.SetActive(false);
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(1f);  // black screen

        // auto dialogue sequence
        string[] lines = {
            "\"But how can I possibly help? I feel so ill-prepared.\"",
            "\"I've been doing a lot of diving recently. I've been seeing some interesting items in the depths.\"",
            "\"Alright. Starting tomorrow, I'll explore and bring back anything I find helpful.\"",
        };

        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return Fade.fadeGraphic(dialogueText, 0.5f, 1);
            yield return new WaitForSeconds(3.5f);
            yield return Fade.fadeGraphic(dialogueText, 0.5f, 0);
        }

        // player click here bc important - only ONE ITEM
        dialogueText.text = "\"The only problem is... I can only carry <b>one item</b> per dive.\"";
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 1);  // fade in text
        yield return new WaitForSeconds(2f);
        yield return WaitForPlayer();  // to click
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 0);  // fade out text

        dialogueText.text =  "\"I'll have to choose wisely.\"";
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 1);  // fade in text
        yield return new WaitForSeconds(2f);
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 0);  // fade out text

        // fade full screen to black
        yield return StartCoroutine(Fade.fadeScreen(blackScreen, 1f));

        // go to next scene (Level 1 / start of playable game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


// -------------------------
//      CLICK WAIT
// -------------------------
    IEnumerator WaitForPlayer()
    {
        bool clicked = false;

        continueButton.gameObject.SetActive(true);

        // start transparent
        Fade.setAlpha(continueButton, 0);

        // fade in button
        yield return StartCoroutine(Fade.fadeGraphic(continueButton.image, 1f, 1f));

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);

        // fade out button
        yield return StartCoroutine(Fade.fadeGraphic(continueButton.image, 1f, 0f));
        continueButton.gameObject.SetActive(false);
    }
}
