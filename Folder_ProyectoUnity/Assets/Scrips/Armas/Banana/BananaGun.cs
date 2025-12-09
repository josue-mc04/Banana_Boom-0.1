using UnityEngine;

public class BananaGun : Weapon
{
    [Header("Punto de disparo")]
    [SerializeField] private Transform firePoint;

    public override void Fire()
    {
        if (!canFire || isReloading || currentAmmo <= 0)
        {
            return;
        }

        ShootProjectile();
        currentAmmo--;

        StartCoroutine(FireCooldown(weaponData.FireRate));

        if (currentAmmo <= 0)
        {
            Reload();
        }
    }

    private void ShootProjectile()
    {
        if (weaponData.ProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("No hay prefab de projectile o firePoint");
            return;
        }

        //instanciar la bala
        GameObject projectileObj = Instantiate(weaponData.ProjectilePrefab, firePoint.position, firePoint.rotation);

        BananaProjectile projectile = projectileObj.GetComponent<BananaProjectile>();

        //asigna damage, velocidad y dueño
        projectile.damage = weaponData.Damage;
        projectile.speed = weaponData.ShootForce;
        projectile.owner = transform.root;

        // ignora la colision entre la bala y el player
        Collider playerCol = transform.root.GetComponentInChildren<Collider>();
        Collider projCol = projectileObj.GetComponent<Collider>();

        if (playerCol != null && projCol != null)
        {
            Physics.IgnoreCollision(projCol, playerCol, true);
        }
    }

}