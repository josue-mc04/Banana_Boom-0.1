using UnityEngine;

public class PapayaBomb : Weapon
{
    [SerializeField] private GameObject papayaPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float throwForce = 12f;
    [SerializeField] private float radius;

    public override void Fire()
    {
        if (ammo > 0)
        {
            ammo--;
            Debug.Log($"{weaponName} lanza una bomba de papaya (radio {radius}m, daño {damage})");

            GameObject papaya = Instantiate(papayaPrefab, shootPoint.position, shootPoint.rotation);
            papaya.GetComponent<Rigidbody>().AddForce(shootPoint.forward * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No hay papayas.");
        }
    }
}
