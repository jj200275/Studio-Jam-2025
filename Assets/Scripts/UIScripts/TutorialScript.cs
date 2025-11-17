using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup keysGroup;
    [SerializeField] private CanvasGroup oxygenInstruction;
    [SerializeField] private CanvasGroup staminaInstruction;
    [SerializeField] private CanvasGroup tutorialEndText;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float keysVisibleTime = 3f;
    [SerializeField] private float instructionVisibleTime = 5f;

    private void Start()
    {
        StartCoroutine(UISequence());
    }

    private IEnumerator UISequence()
    {
        // start with instructions hidden
        oxygenInstruction.alpha = 0;
        staminaInstruction.alpha = 0;
        tutorialEndText.alpha = 0;

        // // Keys fade in (optional if they start already visible)
        // yield return FadeCanvas(keysGroup, 1, fadeDuration);
        // yield return new WaitForSeconds(keysVisibleTime);

        // delay before fade out keys - so player can read
        yield return new WaitForSeconds(7f);

        // keys fade out
        yield return FadeCanvas(keysGroup, 0, fadeDuration);

        // short delay before instructions
        yield return new WaitForSeconds(0.5f);

        // instruction 1 fade in/out - Oxygen
        yield return FadeCanvas(oxygenInstruction, 1, fadeDuration);
        yield return new WaitForSeconds(instructionVisibleTime);
        yield return FadeCanvas(oxygenInstruction, 0, fadeDuration);
        
        // instruction 2 fade in/out - Stamina
        yield return FadeCanvas(staminaInstruction, 1, fadeDuration);
        yield return new WaitForSeconds(instructionVisibleTime);
        yield return FadeCanvas(staminaInstruction, 0, fadeDuration);

        // tutorial end text fade in/out
        yield return FadeCanvas(tutorialEndText, 1, fadeDuration);
        yield return new WaitForSeconds(instructionVisibleTime);
        yield return FadeCanvas(tutorialEndText, 0, fadeDuration);
    }

    private IEnumerator FadeCanvas(CanvasGroup cg, float targetAlpha, float duration)
    {
        float start = cg.alpha;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, targetAlpha, t / duration);
            yield return null;
        }

        cg.alpha = targetAlpha;
    }
}