using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class DayStart : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image itemImage;     // bottle then switch to letter
    [SerializeField] private Image blackScreen;
    [SerializeField] private Button continueButton;

    [SerializeField] private CanvasGroup dayCanvasGroup;
    [SerializeField] private CanvasGroup playerCanvasGroup;
    [SerializeField] private CanvasGroup endCanvasGroup;

    [Header("Sprites")]  // assign sprites in Inspector
    [SerializeField] private Sprite bottleSprite;
    [SerializeField] private Sprite letterSprite;

    [Header("Day Toggles")]
    [SerializeField] private bool isDay2;
    [SerializeField] private bool isDay3;
    [SerializeField] private bool isDay4;
    [SerializeField] private bool isDay5;

    private string[] activeDialogue;
    private int dayNumber;

// -------------------------
//      Dialogue lists
// -------------------------
    private string[] day2Dialogue = new string[]
    {
        "Another letter...",
        "What kind of item should I prioritize today?"
    };

    private string[] day3Dialogue = new string[] { };

    private string[] day4Dialogue = new string[] { };

    private string[] day5Dialogue = new string[]
    {
        "No letter today.",
        "Seth and his friends must be in grave danger.",
        "I'll leave for the laboratory tonight. This will be my last dive."
    };

// -------------------------
    private void Start()
    {
        dayCanvasGroup.Show();
        playerCanvasGroup.Hide();
        endCanvasGroup.Hide();

        DetermineDay();
        StartCoroutine(Sequence());
    }

    private void DetermineDay()
    {
        if (isDay5) { dayNumber = 5; activeDialogue = day5Dialogue; }
        else if (isDay4) { dayNumber = 4; activeDialogue = day4Dialogue; }
        else if (isDay3) { dayNumber = 3; activeDialogue = day3Dialogue; }
        else { dayNumber = 2; activeDialogue = day2Dialogue; }

        dayText.text = "DAY " + dayNumber;
    }

    private IEnumerator Sequence()
    {
        // make everything invisible at start
        Fade.setAlpha(dayText, 0);
        Fade.setAlpha(dialogueText, 0);
        Fade.setAlpha(itemImage, 0);
        Fade.setAlpha(continueButton.image, 0);
        continueButton.gameObject.SetActive(false);

        // DAY #
        yield return Fade.fadeGraphic(dayText, 1f, 1);
        yield return new WaitForSeconds(2f);
        yield return Fade.fadeGraphic(dayText, 1f, 0);

        // bottle
        itemImage.sprite = bottleSprite;
        yield return Fade.fadeGraphic(itemImage, 1f, 1);
        yield return new WaitForSeconds(2f);
        yield return Fade.fadeGraphic(itemImage, 1f, 0);

        // letter
        itemImage.sprite = letterSprite;
        yield return Fade.fadeGraphic(itemImage, 1f, 1);

        // auto dialogue
        foreach (string line in activeDialogue)
        {
            dialogueText.text = line;
            yield return Fade.fadeGraphic(dialogueText, 1f, 1);
            yield return new WaitForSeconds(1.5f);
            yield return Fade.fadeGraphic(dialogueText, 1f, 0);
        }

        // continue button
        continueButton.gameObject.SetActive(true);
        yield return Fade.fadeGraphic(continueButton.image, 1f, 1);
        continueButton.interactable = true;        // ensure button is interactable
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            Debug.Log("Continue button clicked!");
            dayCanvasGroup.Hide();
            playerCanvasGroup.Show();
        });

        // continueButton.onClick.AddListener(() =>
        // {
        //     Debug.Log("Continue button clicked!");

        //     // deactivate THIS canvas group
        //     CanvasGroup current = this.GetComponent<CanvasGroup>();
        //     current.alpha = 0;
        //     current.interactable = false;
        //     current.blocksRaycasts = false;
        //     current.gameObject.SetActive(false); // optional

        //     // activate NEXT canvas group
        //     if (nextCanvasGroup != null)
        //     {
        //         nextCanvasGroup.alpha = 1;
        //         nextCanvasGroup.interactable = true;
        //         nextCanvasGroup.blocksRaycasts = true;
        //         nextCanvasGroup.gameObject.SetActive(true); // must be active
        //     }
        // });
    }
}