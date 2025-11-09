using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float Jumpforce;
    //* direcion

    private void OnCollisionEnter(Collision collision)//-deberia llamar un metodo dentro del player que contenga el AddJumpToDirection
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(transform.up * Jumpforce);

        }
    }
}
