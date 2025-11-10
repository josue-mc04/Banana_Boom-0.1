using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    #region Metodos para los inputs actions
    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchWeapon();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FireCurrentWeapon();
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReloadCurrentWeapon();
        }
    }
    #endregion

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
