using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    [Header("UI Elements")]
    public Image staminaFill; // assign in inspector

    [Header("Stamina Stats")]
    public float maxStamina = 100f;
    public float currentStamina;

    [Header("Deplete Speed")]
    public float depleteSpeed = 5f; // smooth fill animation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    private void Update()
    {
        // For testing: decrease stamina when pressing shift - might have to link to player mvmt code
        if (Input.GetKey(KeyCode.LeftShift))
        {
            DecreaseStamina(20f * Time.deltaTime);
        }

        // smooth UI animation
        staminaFill.fillAmount = Mathf.Lerp(staminaFill.fillAmount, currentStamina / maxStamina, Time.deltaTime * depleteSpeed);
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

    private void UpdateStaminaUI()
    {
        staminaFill.fillAmount = currentStamina / maxStamina;
    }

}
