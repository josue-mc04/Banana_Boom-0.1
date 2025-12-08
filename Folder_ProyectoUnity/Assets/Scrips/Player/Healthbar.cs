using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        if (_healthbarSprite == null)
        {
            return;
        }
        _healthbarSprite.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
    }
}