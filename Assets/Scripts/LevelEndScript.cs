// using UnityEngine;
// using UnityEngine.UI;

// // Adjusted from Justin's fade script in Reminiscence
// public class FadeScript : MonoBehaviour
// {
//     [SerializeField] private Image vignette; // assign in inspector
//     [SerializeField] private float fadeSpeed = 0.8f; 

//     private float fade = 0f; 
//     public bool present = true; // true = fade in, false = fade out

//     void Update()
//     {
//         // adjust fade based on present
//         float target = present ? 1f : 0f;
//         fade = Mathf.MoveTowards(fade, target, fadeSpeed * Time.deltaTime);

//         // alpha for vignette
//         vignette.color = new Color(1f, 1f, 1f, fade);
//     }
// }
//----------------------------------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelEndScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform parentCanvas;     // so can access instantiated prefabs
    [SerializeField] private Image vignette;             // for scene fade-to-black
    [SerializeField] private float screenFadeSpeed = 1f;

    [Header("Achievement Timing")]
    [SerializeField] private float displayTime = 2f;     // time each achievement stays visible
    [SerializeField] private float fadeDuration = 0.5f;  // fade duration for achievement box
    // prob have to add (fixed) array of text here - maybe a dictionary (associate item with category) - "{Item} was collected - +{num_points} in {stamina})
            // associate item with points AND type of buff (e.g. stamina vs speed)?? OR just have num_points earned but not specific to a category?
            // maybe dictionary of item with category, and then manually do points in this script? OR even do both category and points in this script.
    // prob also have to add (fixed) array of icon list here
    // basc, need: item, text, category, num_points, icon.

    [Header("Testing")]
    [SerializeField] private bool testMode = true;       // toggle testmode
    [SerializeField] private GameObject testAchievementPrefab; // prefab to use in test mode
    [SerializeField] private Sprite testIconSprite;      // test icon for prefab

    [System.Serializable]
    public class AchievementData
    {
        public string title;        // achievement text
        public Sprite icon;         // icon of item collected
        public GameObject prefab;
    }
    [HideInInspector] public List<AchievementData> achievements = new List<AchievementData>();

    //---------------------------------------------------------------------------------------------------
    private void Start()
    {
        if (testMode)
        {
            achievements = new List<AchievementData>
            {
                new AchievementData { title = "First Kill", icon = testIconSprite, prefab = testAchievementPrefab },
                new AchievementData { title = "Found Hidden Item", icon = testIconSprite, prefab = testAchievementPrefab },
                new AchievementData { title = "Speed Run Bonus", icon = testIconSprite, prefab = testAchievementPrefab }
            };
            StartCoroutine(LevelEndRoutine());
        }
    }

    public void StartLevelEndSequence()
    {
        if (achievements.Count == 0)
        {
            Debug.LogWarning("No achievements to display!");
        }

        StartCoroutine(LevelEndRoutine());
    }


    private IEnumerator LevelEndRoutine()
    {
        foreach (AchievementData achievement in achievements)
        {
            GameObject box = Instantiate(achievement.prefab, parentCanvas);  // instantiate prefab

            Text titleText = box.GetComponentInChildren<Text>();  // set text
            if (titleText != null)
                titleText.text = achievement.title;

            Image iconImage = box.transform.Find("Icon")?.GetComponent<Image>();  // set icon
            if (iconImage != null && achievement.icon != null)
                iconImage.sprite = achievement.icon;

    
            yield return StartCoroutine(FadeBox(box, 0f, 1f, fadeDuration));  // achievement fade in
            yield return new WaitForSeconds(displayTime);                     // wait for player to read achievement
            yield return StartCoroutine(FadeBox(box, 1f, 0f, fadeDuration));  // achievement fade out

            Destroy(box);  // destroy prefab
        }

        // fade scene to black
        yield return StartCoroutine(FadeScreen(0f, 1f, screenFadeSpeed));

        // load next scene - once get build profile set
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        achievements.Clear();  // clear/reset achievements for next level
    }

    // For fading achievement box
    private IEnumerator FadeBox(GameObject box, float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;

        Image image = box.GetComponent<Image>();
        Text text = box.GetComponentInChildren<Text>();
        Color imageColor = image != null ? image.color : Color.clear;
        Color textColor = text != null ? text.color : Color.clear;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);

            if (image != null)
                image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);

            if (text != null)
                text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

            yield return null;
        }

        // ensure final alpha
        if (image != null)
            image.color = new Color(imageColor.r, imageColor.g, imageColor.b, endAlpha);

        if (text != null)
            text.color = new Color(textColor.r, textColor.g, textColor.b, endAlpha);
    }


    // For fading full scene
    private IEnumerator FadeScreen(float startAlpha, float endAlpha, float speed)
    {
        vignette.gameObject.SetActive(true);
        float fade = startAlpha;

        while (fade < endAlpha)
        {
            fade += speed * Time.deltaTime;
            vignette.color = new Color(0f, 0f, 0f, fade);
            yield return null;
        }

        vignette.color = new Color(0f, 0f, 0f, endAlpha);
    }
}


