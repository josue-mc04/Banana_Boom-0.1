using UnityEngine;

public class PapayaBomb : Weapon
{
    [SerializeField]private float radius;

    public override void Fire()
    {
        if (ammo > 0)
        {
            Debug.Log($"{weaponName} lanza una bomba con radio de {radius}m. Daño: {damage}");
            ammo--;
            Explode();
        }
        else if(ammo <= 0)
        {
            Debug.Log("no hay bombas de papaya.");
        }
    }

    public void Explode()
    {
        Debug.Log("PapayaBomb explota");
    }
}
