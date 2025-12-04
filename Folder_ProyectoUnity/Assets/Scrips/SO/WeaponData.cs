using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    [SerializeField]private string weaponName;

    [Header("Stats")]
    [SerializeField]private int maxAmmo;        
    [SerializeField]private int damage;         
    [SerializeField]private float fireRate = 1; 

    [Header("Visual")]
    [SerializeField]private GameObject projectilePrefab; 
    [SerializeField]private float shootForce = 20f;

    //no importa donde vaya los getters siempre me siguen TT
    public string WeaponName => weaponName;
    public int MaxAmmo => maxAmmo;
    public int Damage => damage;
    public float FireRate => fireRate;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float ShootForce => shootForce;
}
