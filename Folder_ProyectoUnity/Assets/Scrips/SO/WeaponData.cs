using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Info Básica")]
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite weaponIcon; 

    [Header("Estadísticas Comunes")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int damage = 5;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float reloadTime = 2f;

    [Header("Visual")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootForce = 20f;

    [Header("Propiedades Especiales (opcionales)")]
    [SerializeField] private float punchForce = 15f;       
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float stunDuration = 4f;

    //GETTERS pe
    public string WeaponName => weaponName;
    public Sprite WeaponIcon => weaponIcon;
    public int MaxAmmo => maxAmmo;
    public int Damage => damage;
    public float FireRate => fireRate;
    public float ReloadTime => reloadTime;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float ShootForce => shootForce;
    public float PunchForce => punchForce;
    public float ExplosionRadius => explosionRadius;
    public float StunDuration => stunDuration;
}
