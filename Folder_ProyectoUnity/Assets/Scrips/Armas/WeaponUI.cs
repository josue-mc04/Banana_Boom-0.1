using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image weaponImage;

    public void UpdateWeaponIcon(Sprite newIcon)
    {
        weaponImage.sprite = newIcon;
    }
}
