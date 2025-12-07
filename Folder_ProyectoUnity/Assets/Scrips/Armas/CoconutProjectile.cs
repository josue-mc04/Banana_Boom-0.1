/*using UnityEngine;

public class CoconutProjectile : Weapon
{
    [Header("Proyectil Settings")]
    [SerializeField] private GameObject coconutPrefab;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float travelTime;

    [SerializeField] private int stunTime;


    public override void Fire()
    {
        if (ammo <= 0)
        {
            Debug.Log("No hay cocos para lanzar.");
            return;
        }

        ammo--;

        Debug.Log($"{weaponName} lanza un coco curvo y aturde por {stunTime}s");

        GameObject coconut = Instantiate(coconutPrefab, shootPoint.position, shootPoint.rotation);

        CoconutCurve curve = coconut.GetComponent<CoconutCurve>();

        if (curve == null)
        {
            curve = coconut.AddComponent<CoconutCurve>();
        }

        curve.Setup(shootPoint, distance, height, travelTime);
    }

    public override void Reload()
    {
        Debug.Log($"{weaponName} recarga sus cocos.");
    }
}*/
