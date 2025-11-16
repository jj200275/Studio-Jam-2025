using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelEndScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform achievementParent;  // where achievements appear
    [SerializeField] private GameObject achievementPrefab; // UI box prefab
    [SerializeField] private Image fadeScreen;             // fullscreen scene fade-to-black

    [Header("Display Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 1.5f;

    // stores all item achievement icons collected during level/scene
    public List<Sprite> collectedAchievementIcons = new List<Sprite>();

//------------------------------------------- TESTING -------------------------------------------------
    [Header("Testing")]
    [SerializeField] private List<Sprite> testIcons;     // Assign 3 test sprites in Inspector

    private void Start()
    {
        // TEST: Add test icons (assigned in inspector to test) to simulate achievements
        foreach (Sprite icon in testIcons)
        {
            collectedAchievementIcons.Add(icon);
        }

        StartLevelEnd();
    }
//-------------------------------------------------------------------------------------------------------

    // other script calls this (when player "collects" / brings item back up to surface, add to list)
    // in other script, the "Sprite icon" will be calling .achievementIcon on an object that is part of ItemData class 
                                                            // (item UI prefabs attaches this script to item)
    public void AddIconToList(Sprite icon)
    {
        if (icon != null)
        {
            collectedAchievementIcons.Add(icon);
            Debug.Log("Added icon: " + icon.name);  // for debugging
        }
    }

    // to start level end transition - call from other script (in trigger)
    public void StartLevelEnd()
    {
        StartCoroutine(ShowAchievementsThenFade());
    }


    private IEnumerator ShowAchievementsThenFade()
    {
        // Show each achievement from that level
        foreach (Sprite icon in collectedAchievementIcons)
        {
            // create UI box
            GameObject box = Instantiate(achievementPrefab, achievementParent);

            // set icon inside box
        Image boxImage = box.GetComponent<Image>();
            if (boxImage != null)
            {
                boxImage.sprite = icon;
                boxImage.color = new Color(1f, 1f, 1f, 0f); // start transparent
            }
            else
            {
                Debug.LogError("Achievement prefab missing Image component!");
                Destroy(box);
                continue;
            }

            yield return StartCoroutine(FadeUI(boxImage, 0f, 1f, fadeDuration));  // box fade in
            yield return new WaitForSeconds(displayTime);                    // wait for player to read achievement
            yield return StartCoroutine(FadeUI(boxImage, 1f, 0f, fadeDuration));  // box fade out

            Destroy(box);
        }

        // Fade screen to black
        yield return StartCoroutine(FadeScreenToBlack());

        // Load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    // fade func for individual achievement boxes
    private IEnumerator FadeUI(Image img, float start, float end, float duration)
    {
        float time = 0f;
        Color original = img.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, time / duration);
            img.color = new Color(original.r, original.g, original.b, alpha);
            yield return null;
        }

        img.color = new Color(original.r, original.g, original.b, end);
    }

    // fade-to-black func for the full screen
    private IEnumerator FadeScreenToBlack()
    {
        float alpha = 0f;
        fadeScreen.gameObject.SetActive(true);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}

