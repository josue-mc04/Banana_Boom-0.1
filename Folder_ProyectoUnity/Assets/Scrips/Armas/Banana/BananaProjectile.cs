using UnityEngine;

public class BananaProjectile : MonoBehaviour
{
    public float damage = 5f;
    public float speed = 20f;

    [HideInInspector] public Transform owner;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //evitar golpear al mismo player
        if (owner != null && collision.transform == owner)
        {
            return;
        }

        //buscar componente en el PADRE del collider
        PlayerControler pc = collision.collider.GetComponentInParent<PlayerControler>();
        if (pc != null)
        {
            Debug.Log("HIT a: " + pc.name);
            pc.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}