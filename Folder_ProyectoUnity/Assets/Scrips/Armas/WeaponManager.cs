using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    private int currentIndex = 0;

    private void Start()
    {
        if (weapons.Count > 0)
        {
            Debug.Log($"Arma inicial: {weapons[currentIndex].name}");
        }
        else
        {
            Debug.Log("No hay armas asignadas al WeaponManager");
        }
    }

    public void SwitchWeapon()
    {
        if (weapons.Count == 0)
        {
            return;
        }
        currentIndex++;
        if (currentIndex >= weapons.Count)
        {
            currentIndex = 0;
        }

        Debug.Log($"Cambiaste a: {weapons[currentIndex].name}");
    }

    public void FireCurrentWeapon()
    {
        if (weapons.Count == 0)
        {
            return; 
        }

        weapons[currentIndex].Fire();
    }

    public void ReloadCurrentWeapon()
    {
        if (weapons.Count == 0)
        { 
            return; 
        }

        weapons[currentIndex].Reload();
    }
}
