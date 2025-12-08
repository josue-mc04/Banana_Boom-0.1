using UnityEngine;

public class BananaProjectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public Transform owner;
    [HideInInspector] public float speed = 20f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //disparo asistido
    public void ShootTowards(Vector3 target)
    {
        if (rb == null) return;

        Vector3 direction = (target - transform.position).normalized;
        rb.linearVelocity = direction * speed;
        transform.forward = direction; //la banana mire al objetivo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(owner)) return; //ignorar al jugador y sus hijos

        PlayerControler player = other.GetComponent<PlayerControler>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
