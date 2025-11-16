using UnityEngine;
using UnityEngine.UI;

public class StaminaScript2 : MonoBehaviour
{
    [Header("UI Elements")]
    public Image staminaFill; // assign in inspector

    [Header("Stamina Stats")]
    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina; // Current stamina is managed publicly

    [Header("UI Speed")]
    public float uiUpdateSpeed = 5f; // smooth fill animation

    void Start()
    {
        currentStamina = maxStamina;
        staminaFill.fillAmount = currentStamina / maxStamina; // Start full
    }

    private void Update()
    {
        // Smoothly update the UI fill bar to match the current stamina value
        float targetFill = currentStamina / maxStamina;
        staminaFill.fillAmount = Mathf.Lerp(staminaFill.fillAmount, targetFill, Time.deltaTime * uiUpdateSpeed);
    }

    public void DecreaseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void RefillStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }
}