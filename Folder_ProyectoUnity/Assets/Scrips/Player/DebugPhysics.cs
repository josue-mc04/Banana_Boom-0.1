using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class DebugPhysics : MonoBehaviour
{
    [SerializeField] private Vector3 linearSpeed;
    [SerializeField] private Vector3 acceleration;
    [SerializeField] private float maxLinearVelocity;
    [SerializeField] private Vector3 maxLinearSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //rb.maxLinearVelocity = maxLinearVelocity;
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += acceleration * Time.deltaTime;
        }

        linearSpeed = rb.linearVelocity;
        maxLinearVelocity = rb.maxLinearVelocity;
        maxLinearSpeed = Vector3.one * maxLinearVelocity;
    }
}