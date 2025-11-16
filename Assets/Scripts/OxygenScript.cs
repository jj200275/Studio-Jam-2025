using UnityEngine;
using UnityEngine.UI;

public class OxygenScript : MonoBehaviour
{
    public Image[] bubbles;  // assign manually in inspector
    public int currentOxygen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentOxygen = bubbles.Length;

        // start scene with full oxygen
        foreach (var bub in bubbles)
            bub.fillAmount = 1f;
    }

    private void Update()
    {
        // For testing!! - if press space, should delete/deplete one oxygen bubble
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void updateBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].fillAmount = (i < currentOxygen) ? 1f : 0f;  // fills bubbles with amt of oxygen player has
        }
    }

    public void TakeDamage(int amount)
    {
        currentOxygen = Mathf.Clamp(currentOxygen - amount, 0, bubbles.Length);  // keeps health between min/max range
        updateBubbles();
    }
}

