using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Propiedades del proyectil")]
    public float fuerzaLanzamiento = 15f;
    public float tiempoVida = 3f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        Destroy(gameObject, tiempoVida);
    }
}
