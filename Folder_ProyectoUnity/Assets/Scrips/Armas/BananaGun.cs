using System.Collections;
using UnityEngine;

public class BananaGun : Weapon
{
    [Header("Configuración Banana")]
    [SerializeField] private Transform shootPoint;

    private bool isReloading = false;

    public override void Fire()
    {
        // 1. Verificar condiciones
        if (!canFire || isReloading || currentAmmo <= 0) return;

        // 2. Usar datos del SO
        if (weaponData == null) return;

        // 3. Restar munición
        currentAmmo--;
        Debug.Log(weaponData.WeaponName + " dispara. Daño: " + weaponData.Damage);

        // 4. Crear banana desde el SO
        if (weaponData.ProjectilePrefab != null && shootPoint != null)
        {
            GameObject banana = Instantiate(
                weaponData.ProjectilePrefab,
                shootPoint.position,
                shootPoint.rotation
            );

            // 5. Aplicar fuerza desde el SO
            Rigidbody rb = banana.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootPoint.forward * weaponData.ShootForce, ForceMode.Impulse);
            }
        }

        // 6. Cooldown desde el SO
        StartCoroutine(FireCooldown(weaponData.FireRate));
    }

    public override void Reload()
    {
        if (isReloading || weaponData == null) return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        Debug.Log(weaponData.WeaponName + " recargando...");

        // Tiempo de recarga desde el SO
        yield return new WaitForSeconds(weaponData.ReloadTime);

        currentAmmo = weaponData.MaxAmmo;
        isReloading = false;
        Debug.Log(weaponData.WeaponName + " recargada!");
    } 
}
