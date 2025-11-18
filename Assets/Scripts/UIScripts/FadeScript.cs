using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class Fade : MonoBehaviour
{
// -------------------------
//       BLACK FADE
// -------------------------
    // Fades to BLACK
    public static IEnumerator fadeScreen(Graphic fade, float fadeDuration)
    {
         // fade to black
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fade.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t / fadeDuration));
            yield return null;
        }
        fade.color = Color.black;
    }

    // Fades from BLACK back to transparent
     public static IEnumerator unfadeScreen(Graphic fade, float fadeDuration)
    {
        // fade back to transparent
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fade.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t / fadeDuration));
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 0);
    }

// -------------------------
//      FADE WITH COLOR
// -------------------------
    // fades but PRESERVES COLOR - targetAlpha = 1 to make visible, 0 to make transparent
    public static IEnumerator fadeGraphic(Graphic graphic, float duration, float targetAlpha)
    {
        float t = 0;
        Color originalColor = graphic.color; // keep original RGB
        float startAlpha = originalColor.a;

        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
            yield return null;
        }

        graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }

// ------------------------------
// FADE CanvasGroup USING ALPHA
// ------------------------------
    public static IEnumerator fadeCanvas(CanvasGroup canvas, float fadeDuration, float targetAlpha)
    {
        // make canvas interactable while visible
        canvas.interactable = targetAlpha > 0;
        canvas.blocksRaycasts = targetAlpha > 0;

        float t = 0f;
        float startAlpha = canvas.alpha;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        canvas.alpha = targetAlpha;
    }

// -------------------------
//      SET ALPHA
// -------------------------
    public static void setAlpha(Graphic graphic, float alpha)
    {
        // preserve original RGB
        Color c = graphic.color;
        c.a = Mathf.Clamp01(alpha); // ensures 0-1
        graphic.color = c;
    }

     public static void setAlpha(Button button, float alpha)
    {
        // preserve original RGB
        Color c = button.image.color;
        c.a = 0f;
        button.image.color = c;
    }

    // // Overload for TextMeshProUGUI
    // public static IEnumerator fadeGraphic(TMP_Text text, float duration, float targetAlpha)
    // {
    //     float t = 0;
    //     Color originalColor = text.color;
    //     float startAlpha = originalColor.a;

    //     while (t < duration)
    //     {
    //         t += Time.deltaTime;
    //         float a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
    //         text.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
    //         yield return null;
    //     }

    //     text.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    // }
}
