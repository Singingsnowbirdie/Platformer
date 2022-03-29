using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        EventManager.OnHealthAmountChangedEvent += UpdateHealthBar;
    } 
    
    private void OnDisable()
    {
        EventManager.OnHealthAmountChangedEvent -= UpdateHealthBar;
    }

    void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float ratio = currentHealth / maxHealth;
        healthBar.rectTransform.localPosition = new Vector3(healthBar.rectTransform.rect.width * ratio - healthBar.rectTransform.rect.width, 0, 0);
        healthText.text = currentHealth.ToString("0") + "/" + maxHealth.ToString("0");
    }
}
