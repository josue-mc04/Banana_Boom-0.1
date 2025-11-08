using UnityEngine;

public class BananaGun : Weapon
{
    [SerializeField]private int range;

    public override void Fire()
    {
        if (ammo > 0)
        {
            Debug.Log(weaponName + " dispara una banana a " + range + "m. Daño: " + damage);
            ammo--;
        }
        else if (ammo <= 0)
        {
            Debug.Log("Hijo, como vas a disparar si no tienes nada");
        }
    }
}
