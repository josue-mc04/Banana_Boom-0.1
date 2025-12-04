using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] allWeaponsInInspector;
    [SerializeField] private Transform weaponHolder;

    [SerializeField] private WeaponUI weaponUI;

    //instanciamos
    private CircularList<Weapon> weaponCircularList = new CircularList<Weapon>();

    private void Start()
    {
        //agregar las armas a la lista circular
        foreach (Weapon w in allWeaponsInInspector)
        {
            weaponCircularList.Add(w);
            w.gameObject.SetActive(false);
            w.transform.SetParent(weaponHolder, false);
            w.transform.localPosition = Vector3.zero;//coloca el arma en weaponHolder
            w.transform.localRotation = Quaternion.identity;
        }

        //equipa la primera arma
        weaponCircularList.GetCurrent()?.Equip();
        weaponCircularList.GetCurrent()?.gameObject.SetActive(true);

        weaponUI.UpdateWeaponIcon(weaponCircularList.GetCurrent().weaponIcon);
    }

    public void OnNextWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        //desactiva lo visual de larma actual
        weaponCircularList.GetCurrent()?.gameObject.SetActive(false);

        //avanza circulamente
        weaponCircularList.Next();

        //activa y equipar la nueva arma
        weaponCircularList.GetCurrent()?.Equip();
        weaponCircularList.GetCurrent()?.gameObject.SetActive(true);

        weaponUI.UpdateWeaponIcon(weaponCircularList.GetCurrent().weaponIcon);
    }

    public void OnPreviousWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        { 
            return;
        }
        //desactiva lo visual del arma
        weaponCircularList.GetCurrent()?.gameObject.SetActive(false);
        //pa retroceder un nodo, lo mismo q el anterior
        weaponCircularList.Prev();
        weaponCircularList.GetCurrent()?.Equip();
        weaponCircularList.GetCurrent()?.gameObject.SetActive(true);

        weaponUI.UpdateWeaponIcon(weaponCircularList.GetCurrent().weaponIcon);
    }

    public void OnFireWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //llamo al metodo del arma actual, cuando presiono el boton de disparo
            weaponCircularList.GetCurrent()?.Fire();
        }
    }

    public void OnReloadWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponCircularList.GetCurrent()?.Reload();
        }
    }
}
