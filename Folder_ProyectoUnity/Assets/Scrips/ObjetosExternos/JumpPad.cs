/*using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float Jumpforce;
    private void OnCollisionEnter(Collision collision)//-deberia llamar un metodo dentro del player que contenga el AddJumpToDirection
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControler>().canJump = true;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerControler>().canJump = false;
    }
}
*/