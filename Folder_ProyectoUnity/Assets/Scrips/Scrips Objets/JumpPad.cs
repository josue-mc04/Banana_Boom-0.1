using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float Jumpforce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(transform.up * Jumpforce);

        }
    }
}
