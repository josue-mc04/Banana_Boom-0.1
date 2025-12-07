using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iweapon
{
    void Fire();
    void Reload();
    void Equip();
}
public abstract class Weapon : MonoBehaviour, Iweapon
{
    [Header("Datos del Arma")]
    [ SerializeField]public WeaponData weaponData;

    [Header("Estado")]
    [SerializeField]public int currentAmmo;
    [SerializeField]public bool canFire = true;

    public Sprite weaponIcon => weaponData != null ? weaponData.WeaponIcon : null;

    protected virtual void Start()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.MaxAmmo;
        }
    }

    private void OnEnable()
    {
        canFire = true;

        if (weaponData != null)
            currentAmmo = weaponData.MaxAmmo;
    }

    public abstract void Fire();

    public virtual void Reload()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.MaxAmmo;
            Debug.Log(weaponData.WeaponName + " recargada");
        }
    }

    public virtual void Equip()
    {
        if (weaponData != null)
        {
            Debug.Log(weaponData.WeaponName + " equipada");
        }
    }

    protected IEnumerator FireCooldown(float fireRate)
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}