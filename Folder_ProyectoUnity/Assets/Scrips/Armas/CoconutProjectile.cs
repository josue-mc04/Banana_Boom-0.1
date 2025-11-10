using UnityEngine;

public class CoconutProjectile : Weapon
{
    [SerializeField] private GameObject coconutPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 15f;
    [SerializeField] private int stunTime;

    public override void Fire()
    {
        ammo--;
        Debug.Log($"{weaponName} lanza un coco que aturde por {stunTime}s");

        GameObject coconut = Instantiate(coconutPrefab, shootPoint.position, shootPoint.rotation);
        coconut.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
    }

    public override void Reload()
    {
        Debug.Log($"{weaponName} recarga sus cocos.");
    }
}
