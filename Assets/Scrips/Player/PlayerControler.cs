using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement Player")]
    [SerializeField] private float speed;
    private Vector2 moveInput;

    [Header("Jump Player")]
    [SerializeField] private int jumpForce;
    private Rigidbody rb;
    private bool canJump;

    [Header("Raycast")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    [Header("Rotacion de camara pal player dx")]
    public Transform camara;
    private void OnEnable()
    {
        InputHandler.OnMove += Move;
        InputHandler.OnJump += Jump;
    }
    private void OnDisable()
    {
        InputHandler.OnMove -= Move;
        InputHandler.OnJump -= Jump;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position,Vector3.down,distance, layer))
        {
            if (canJump)
            {
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            }
        }
    }
    private void LateUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x* speed, rb.linearVelocity.y, moveInput.y * speed);

    }
    public void Move(Vector2 direction)
    {
        moveInput = direction;
    }

    public void Jump(bool value)
    {
            canJump = value;
    }
}