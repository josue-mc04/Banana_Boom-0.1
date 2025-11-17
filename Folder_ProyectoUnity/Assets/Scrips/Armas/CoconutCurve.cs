using UnityEngine;

public class CoconutCurve : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    [SerializeField] private float height;
    [SerializeField] private float timeToTravel;
    private float timer;

    public void Setup(Transform shootPoint, float distance, float height, float travelTime)
    {
        this.height = height;
        this.timeToTravel = travelTime;

        startPos = shootPoint.position;
        endPos = shootPoint.position + shootPoint.forward * distance;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / timeToTravel);

        Vector3 horizontal = Vector3.Lerp(startPos, endPos, t);

        float vertical = Mathf.Sin(t * Mathf.PI) * height;

        transform.position = new Vector3(horizontal.x, horizontal.y + vertical, horizontal.z);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
