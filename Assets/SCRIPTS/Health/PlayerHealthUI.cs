using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Slider healthSlider;

    
    private int lastRecordedHealth = -1;

    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}";
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        
        if (lastRecordedHealth != -1 && currentHealth < lastRecordedHealth && currentHealth > 0)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPlayerDamage();
            }
        }

        
        lastRecordedHealth = currentHealth;
    }
}
