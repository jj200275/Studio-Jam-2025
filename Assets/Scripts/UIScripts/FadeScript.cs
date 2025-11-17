using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public static IEnumerator fadeScreen(Image fade, float fadeDuration)
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

     public static IEnumerator unfadeScreen(Image fade, float fadeDuration)
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
}
