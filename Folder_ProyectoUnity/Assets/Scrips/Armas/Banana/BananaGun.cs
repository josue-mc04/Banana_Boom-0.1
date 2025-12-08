using System.Collections;
using UnityEngine;

public class BananaGun : Weapon
{
    [Header("Punto de disparo")]
    [SerializeField]private Transform firePoint;

    public override void Fire()
    {
        if (!canFire || isReloading || currentAmmo <= 0 || weaponData == null)
            return;

        ShootProjectile();

        currentAmmo--;
        StartCoroutine(FireCooldown(weaponData.FireRate));

        if (currentAmmo <= 0)
            Reload();
    }

    private void ShootProjectile()
    {
        if (weaponData.ProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("No hay prefab o firePoint asignado en " + weaponData.WeaponName);
            return;
        }

        //instancia la banana
        GameObject projectileObj = Instantiate(
            weaponData.ProjectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        BananaProjectile projectile = projectileObj.GetComponent<BananaProjectile>();
        if (projectile != null)
        {
            projectile.damage = weaponData.Damage;
            projectile.owner = transform.root; //el player
            projectile.speed = weaponData.ShootForce;

            //apunta hacia donde mira el jugador
            Vector3 aimPoint = firePoint.position + firePoint.forward * 50f; //ayudita
            projectile.ShootTowards(aimPoint);
        }
    }
}
