using System.Collections;
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
    [SerializeField] public WeaponData weaponData;

    [Header("Estado")]
    [SerializeField] public int currentAmmo;
    [SerializeField] public bool canFire = true;
    protected bool isReloading = false;

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
        isReloading = false;

        if (weaponData != null)
            currentAmmo = weaponData.MaxAmmo;
    }

    public abstract void Fire();

    public virtual void Reload()
    {
        if (isReloading || weaponData == null) return;

        isReloading = true;
        Debug.Log(weaponData.WeaponName + " recargando...");

        //recarga con delay usando Coroutine
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(weaponData.ReloadTime);
        currentAmmo = weaponData.MaxAmmo;
        isReloading = false;
        Debug.Log(weaponData.WeaponName + " recargada");
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
