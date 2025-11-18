using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StartDialogue : MonoBehaviour
{
    [Header("Canvas Groups")]
    public CanvasGroup currentCanvasGroup;
    public CanvasGroup nextCanvasGroup; 

    [Header("Main UI Objects")]
    public Image mainImage;          // this will switch from bottle to letter
    public Image blackScreen;        // use this for fadeScreen()

    [Header("Sprites")]
    public Sprite bottleSprite;
    public Sprite letterZeroSprite;

    [Header("Dialogue")]
    public TMP_Text dialogueText;
    public Button continueButton;

    private void Start()
    {
        // initialize transparencies
        Fade.setAlpha(mainImage, 0);
        Fade.setAlpha(dialogueText, 0);

        blackScreen.color = Color.black;   // start black
        continueButton.gameObject.SetActive(false);

        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(1f);  // black screen

        // reveal bottle
        mainImage.sprite = bottleSprite;
        yield return Fade.fadeGraphic(mainImage, 1f, 1);

        // start dialogue
        dialogueText.text = "\"What's this?\"";
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 1);
        yield return new WaitForSeconds(2f);
        yield return WaitForPlayer();  // to click
        yield return Fade.fadeGraphic(mainImage, 0.5f, 0);
        yield return Fade.fadeGraphic(dialogueText, 0.5f, 0);

        // switch sprite to Letter 0
        mainImage.sprite = letterZeroSprite;
        yield return Fade.fadeGraphic(mainImage, 1f, 1);
        yield return new WaitForSeconds(3f);
        yield return Fade.fadeGraphic(mainImage, 1f, 0);

        // auto dialogue sequence
        string[] lines = {
            "\"Is that...\"",
            "\"Is that Seth???\"",
            "\"Oh no. It looks like he and his friends have been abducted.\"",
            "\"I've got to find a way to save them.\""
        };

        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return Fade.fadeGraphic(dialogueText, 0.5f, 1);
            // yield return WaitForPlayer();
            yield return new WaitForSeconds(2.5f);
            yield return Fade.fadeGraphic(dialogueText, 0.5f, 0);
        }

        // fade full screen to black
        yield return StartCoroutine(Fade.fadeScreen(blackScreen, 1f));

        // switch canvas groups
        currentCanvasGroup.gameObject.SetActive(false);
        nextCanvasGroup.gameObject.SetActive(true);
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

