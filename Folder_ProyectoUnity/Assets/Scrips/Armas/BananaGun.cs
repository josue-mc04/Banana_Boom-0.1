using UnityEngine;

public class BananaGun : Weapon
{
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 20f;
    [SerializeField] private int range;

    public override void Fire()
    {
        if (ammo > 0)
        {
            ammo--;
            Debug.Log($"{weaponName} dispara una banana. Alcance: {range}m, Daño: {damage}");

            GameObject banana = Instantiate(bananaPrefab, shootPoint.position, shootPoint.rotation);
            banana.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No hay bananas para disparar");
        }
    }
}
