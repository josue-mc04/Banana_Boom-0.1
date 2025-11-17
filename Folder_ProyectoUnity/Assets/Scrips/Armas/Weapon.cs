using System.Collections.Generic;
using UnityEngine;

public interface Iweapon
{
    void Fire();
    void Reload();
    void Equip();
}
public class Weapon : MonoBehaviour, Iweapon
{
    [Header("Weapon stats")] //estadistica de arma
    protected string weaponName;
    protected int ammo = 20;
    protected int damage = 5;

    public virtual void Fire()
    {
        Debug.Log(weaponName + " dispara. Dano: " + damage);
    }

    public virtual void Reload()
    {
        Debug.Log(weaponName + " recargado");
    }

    public virtual void Equip()
    {
        Debug.Log(weaponName + " equipada.");
    }
    
}
