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

        // continueButton.onClick.RemoveAllListeners();
        // continueButton.onClick.AddListener(() =>
        // {
        //     // StartCoroutine(FadeToBlackAndExit());
        // });
    }

    // private IEnumerator FadeToBlackAndExit()
    // {
    //     yield return Fade.fadeGraphic(blackScreen, 1f, 1);
    //     gameObject.SetActive(false);
    // }
}



// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections;

// public class DayStart : MonoBehaviour
// {
//     [Header("UI References")]
//     [SerializeField] private TMP_Text dayText;
//     [SerializeField] private TMP_Text dialogueText;
//     [SerializeField] private Image itemImage;     // bottle then letter
//     [SerializeField] private Button continueButton;

//     [Header("CanvasGroups")]
//     [SerializeField] private CanvasGroup dayCanvasGroup;      // this UI
//     [SerializeField] private CanvasGroup playerCanvasGroup;   // player bars UI

//     [Header("ActivateCanvasGroup Script")]
//     [SerializeField] private ActivateCanvasGroup endGroupScript; // your end-level script

//     [Header("Sprites")]
//     [SerializeField] private Sprite bottleSprite;
//     [SerializeField] private Sprite letterSprite;

//     [Header("Day Toggles")]
//     [SerializeField] private bool isDay2;
//     [SerializeField] private bool isDay3;
//     [SerializeField] private bool isDay4;
//     [SerializeField] private bool isDay5;

//     private string[] activeDialogue;
//     private int dayNumber;

//     // Dialogue lists
//     private string[] day2Dialogue = new string[]
//     {
//         "Another letter...",
//         "What kind of item should I prioritize today?"
//     };
//     private string[] day3Dialogue = new string[] { };
//     private string[] day4Dialogue = new string[] { };
//     private string[] day5Dialogue = new string[]
//     {
//         "No letter today.",
//         "Seth and his friends must be in grave danger.",
//         "I'll leave for the laboratory tonight. This will be my last dive."
//     };

//     private void Start()
//     {
//         // Initial states
//         ShowCanvasGroup(dayCanvasGroup);
//         HideCanvasGroup(playerCanvasGroup);

//         DetermineDay();
//         StartCoroutine(Sequence());
//     }

//     private void DetermineDay()
//     {
//         if (isDay5) { dayNumber = 5; activeDialogue = day5Dialogue; }
//         else if (isDay4) { dayNumber = 4; activeDialogue = day4Dialogue; }
//         else if (isDay3) { dayNumber = 3; activeDialogue = day3Dialogue; }
//         else { dayNumber = 2; activeDialogue = day2Dialogue; }

//         dayText.text = "DAY " + dayNumber;
//     }

//     private IEnumerator Sequence()
//     {
//         // Make UI elements invisible at start
//         SetAlpha(dayText, 0);
//         SetAlpha(dialogueText, 0);
//         SetAlpha(itemImage, 0);
//         SetAlpha(continueButton.image, 0);

//         continueButton.gameObject.SetActive(false);
//         continueButton.interactable = false;

//         // -------------------
//         // Fade in Day #
//         // -------------------
//         yield return StartCoroutine(FadeGraphic(dayText, 1f, 1f));
//         yield return new WaitForSeconds(2f);
//         yield return StartCoroutine(FadeGraphic(dayText, 1f, 0f));

//         // -------------------
//         // Fade in Bottle
//         // -------------------
//         itemImage.sprite = bottleSprite;
//         yield return StartCoroutine(FadeGraphic(itemImage, 1f, 1f));
//         yield return new WaitForSeconds(2f);
//         yield return StartCoroutine(FadeGraphic(itemImage, 1f, 0f));

//         // -------------------
//         // Fade in Letter
//         // -------------------
//         itemImage.sprite = letterSprite;
//         yield return StartCoroutine(FadeGraphic(itemImage, 1f, 1f));

//         // -------------------
//         // Auto dialogue
//         // -------------------
//         foreach (string line in activeDialogue)
//         {
//             dialogueText.text = line;
//             yield return StartCoroutine(FadeGraphic(dialogueText, 1f, 1f));
//             yield return new WaitForSeconds(1.5f);
//             yield return StartCoroutine(FadeGraphic(dialogueText, 1f, 0f));
//         }

//         // -------------------
//         // Show Continue button
//         // -------------------
//         continueButton.gameObject.SetActive(true);
//         continueButton.interactable = true;
//         yield return StartCoroutine(FadeGraphic(continueButton.image, 1f, 1f));

//         continueButton.onClick.RemoveAllListeners();
//         continueButton.onClick.AddListener(() =>
//         {
//             // Hide DayStart UI
//             HideCanvasGroup(dayCanvasGroup);

//             // Show PlayerBars UI
//             ShowCanvasGroup(playerCanvasGroup);

//             // Signal end-level script
//             if (endGroupScript != null)
//             {
//                 endGroupScript.playerReachedEnd = true;
//             }
//         });
//     }

//     // -------------------
//     // Helper functions
//     // -------------------
//     private void ShowCanvasGroup(CanvasGroup cg)
//     {
//         if (cg == null) return;
//         cg.alpha = 1f;
//         cg.interactable = true;
//         cg.blocksRaycasts = true;
//         cg.gameObject.SetActive(true);
//     }

//     private void HideCanvasGroup(CanvasGroup cg)
//     {
//         if (cg == null) return;
//         cg.alpha = 0f;
//         cg.interactable = false;
//         cg.blocksRaycasts = false;
//         cg.gameObject.SetActive(false);
//     }

//     private void SetAlpha(Graphic g, float alpha)
//     {
//         if (g == null) return;
//         Color c = g.color;
//         c.a = alpha;
//         g.color = c;
//     }

//     private IEnumerator FadeGraphic(Graphic g, float duration, float targetAlpha)
//     {
//         if (g == null) yield break;

//         float startAlpha = g.color.a;
//         float elapsed = 0f;

//         while (elapsed < duration)
//         {
//             elapsed += Time.deltaTime;
//             float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
//             SetAlpha(g, alpha);
//             yield return null;
//         }

//         SetAlpha(g, targetAlpha);
//     }
// }




// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections;


// public class DayStart : MonoBehaviour
// {
//     [Header("UI References")]
//     [SerializeField] private TMP_Text dayText;
//     [SerializeField] private TMP_Text dialogueText;
//     [SerializeField] private Image itemImage;     // bottle then switch to letter
//     [SerializeField] private Image blackScreen;
//     [SerializeField] private Button continueButton;

//     [SerializeField] private CanvasGroup dayCanvasGroup;
//     [SerializeField] private CanvasGroup playerCanvasGroup;
//     [SerializeField] private CanvasGroup endCanvasGroup;

//     [Header("Sprites")]  // assign sprites in Inspector
//     [SerializeField] private Sprite bottleSprite;
//     [SerializeField] private Sprite letterSprite;

//     [Header("Day Toggles")]
//     [SerializeField] private bool isDay2;
//     [SerializeField] private bool isDay3;
//     [SerializeField] private bool isDay4;
//     [SerializeField] private bool isDay5;

//     private string[] activeDialogue;
//     private int dayNumber;

// // -------------------------
// //      Dialogue lists
// // -------------------------
//     private string[] day2Dialogue = new string[]
//     {
//         "Another letter...",
//         "What kind of item should I prioritize today?"
//     };

//     private string[] day3Dialogue = new string[] { };

//     private string[] day4Dialogue = new string[] { };

//     private string[] day5Dialogue = new string[]
//     {
//         "No letter today.",
//         "Seth and his friends must be in grave danger.",
//         "I'll leave for the laboratory tonight. This will be my last dive."
//     };

// // -------------------------
//     private void Start()
//     {
//         dayCanvasGroup.Show();
//         playerCanvasGroup.Hide();
//         endCanvasGroup.Hide();

//         DetermineDay();
//         StartCoroutine(Sequence());
//     }

//     private void DetermineDay()
//     {
//         if (isDay5) { dayNumber = 5; activeDialogue = day5Dialogue; }
//         else if (isDay4) { dayNumber = 4; activeDialogue = day4Dialogue; }
//         else if (isDay3) { dayNumber = 3; activeDialogue = day3Dialogue; }
//         else { dayNumber = 2; activeDialogue = day2Dialogue; }

//         dayText.text = "DAY " + dayNumber;
//     }

//     private IEnumerator Sequence()
//     {
//         // make everything invisible at start
//         Fade.setAlpha(dayText, 0);
//         Fade.setAlpha(dialogueText, 0);
//         Fade.setAlpha(itemImage, 0);
//         Fade.setAlpha(continueButton.image, 0);
//         continueButton.gameObject.SetActive(false);

//         // DAY #
//         yield return Fade.fadeGraphic(dayText, 1f, 1);
//         yield return new WaitForSeconds(2f);
//         yield return Fade.fadeGraphic(dayText, 1f, 0);

//         // bottle
//         itemImage.sprite = bottleSprite;
//         yield return Fade.fadeGraphic(itemImage, 1f, 1);
//         yield return new WaitForSeconds(2f);
//         yield return Fade.fadeGraphic(itemImage, 1f, 0);

//         // letter
//         itemImage.sprite = letterSprite;
//         yield return Fade.fadeGraphic(itemImage, 1f, 1);

//         // auto dialogue
//         foreach (string line in activeDialogue)
//         {
//             dialogueText.text = line;
//             yield return Fade.fadeGraphic(dialogueText, 1f, 1);
//             yield return new WaitForSeconds(1.5f);
//             yield return Fade.fadeGraphic(dialogueText, 1f, 0);
//         }

//         // continue button
//         continueButton.gameObject.SetActive(true);
//         yield return Fade.fadeGraphic(continueButton.image, 1f, 1);
//         continueButton.interactable = true;        // ensure button is interactable
//         continueButton.onClick.RemoveAllListeners();
//         // continueButton.onClick.AddListener(() =>
//         // {
//         //     Debug.Log("Continue button clicked!");
//         //     dayCanvasGroup.Hide();
//         //     playerCanvasGroup.Show();
//         // });

//         continueButton.onClick.AddListener(() =>
//         {
//             Debug.Log("Continue button clicked!");

//             // activate NEXT canvas group
//             if (playerCanvasGroup != null)
//             {
//                 playerCanvasGroup.alpha = 1;
//                 playerCanvasGroup.interactable = true;
//                 playerCanvasGroup.blocksRaycasts = true;
//                 playerCanvasGroup.gameObject.SetActive(true); // must be active
//             }

//             // deactivate day canvas group
//             dayCanvasGroup.alpha = 0;
//             dayCanvasGroup.interactable = false;
//             dayCanvasGroup.blocksRaycasts = false;
//             dayCanvasGroup.gameObject.SetActive(false); // optional
//         });
//     }
// }