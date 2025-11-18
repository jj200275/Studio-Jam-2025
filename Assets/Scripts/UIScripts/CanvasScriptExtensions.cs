using UnityEngine;

public static class CanvasGroupExtensions
{
    // make a CanvasGroup visible
    public static void Show(this CanvasGroup cg)
    {
        if (cg == null) return;
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    // make a CanvasGroup invisible
    public static void Hide(this CanvasGroup cg)
    {
        if (cg == null) return;
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
