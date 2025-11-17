using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LetterCutscreen : MonoBehaviour
{
    [Header("UI References")]
    public Image mainImage;       // image BEFORE fade - bottle
    public Sprite newSprite;      // image to show AFTER fade - letter0
    public Image fade;            // fullscreen black fade
    public float fadeDuration = 2f;

    [Header("Continue Button")]
    public Button buttonToActivate; // show button after fade and swap
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sfx;         // paper rustling sfx


    void OnEnable()  // should auto start when enable this GameObject
    {
        StartFadeAndSwap();
    }

    public void StartFadeAndSwap()
    {
        StartCoroutine(FadeAndSwapCoroutine());
    }

    private IEnumerator FadeAndSwapCoroutine()
    {
        // play sfx - TODO: INCLUDE ONCE GET (in inspector)!!
        if (audioSource && sfx)
            audioSource.PlayOneShot(sfx);

        // fade to black
        yield return new WaitForSeconds(1f); // delay before fade
        yield return Fade.fadeScreen(fade, fadeDuration);

        // swap image
        mainImage.sprite = newSprite;

        // fade back to transparent
        yield return Fade.unfadeScreen(fade, fadeDuration);

         // activate the button after fade
        yield return new WaitForSeconds(1f); // delay before showing button
        if (buttonToActivate != null)
            buttonToActivate.gameObject.SetActive(true);
    }
}
