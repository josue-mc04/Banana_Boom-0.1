using UnityEngine;
using System.Collections;
public class PapayaBomb : Weapon
{
    [Header("Prefabs y Puntos")]
    [SerializeField] private GameObject papayaPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Movimiento Curvo")]
    [SerializeField] private float travelTime = 1.2f;
    [SerializeField] private float distance = 6f;
    [SerializeField] private AnimationCurve heightCurve;

    private void Reset()
    {
    
        heightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }


    public override void Fire()
    {
        if (ammo <= 0)
        {
            Debug.Log("No hay papayas.");
            return;
        }

        ammo--;

        GameObject papaya = Instantiate(papayaPrefab, shootPoint.position, shootPoint.rotation);

        StartCoroutine(MovePapayaCurve(papaya));
    }

    private IEnumerator MovePapayaCurve(GameObject papaya)
    {
        Vector3 startPos = shootPoint.position;
        Vector3 endPos = shootPoint.position + shootPoint.forward * distance;

        float elapsed = 0f;

        while (elapsed < travelTime)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / travelTime; 


            Vector3 pos = Vector3.Lerp(startPos, endPos, t);


            pos.y += heightCurve.Evaluate(t);

            papaya.transform.position = pos;

            yield return null;
        }

        Destroy(papaya);
    }
}