using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement Player")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float climbSpeed;
    private float currentSpeed;
    private Vector2 moveInput;
   

    [Header("Jump Player")]
    [SerializeField] private int jumpForce;
    private Rigidbody rb;
    private bool canJump;


    [Header("Raycast")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    [SerializeField] private CameraTarget camera;


    private bool isRun;
    private bool isClimb;
    private void OnEnable()
    {
        InputHandler.OnMove += HandleMove;
        InputHandler.OnJump += HandleJump;
        InputHandler.OnRun += HandleRun;
        camera.OnRotationCamera += RotatePlayer;
    }
    private void OnDisable()
    {
        InputHandler.OnMove -= HandleMove;
        InputHandler.OnJump -= HandleJump;
        InputHandler.OnRun -= HandleRun;
       // camera.OnRotationCamera -= RotatePlayer;
    }

    /*private void RotatePlayer()
    {
        // Tomamos la dirección frontal de la cámara, ignorando el eje Y
        Vector3 camForward = camera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        // Solo rotamos si hay movimiento (evita rotación cuando está quieto)
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Calculamos la dirección deseada según entrada y cámara
            Vector3 moveDir = camForward * moveInput.y + camera.transform.right * moveInput.x;
            moveDir.y = 0f;

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
    }*/

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        if (isClimb)
        {
            rb.linearVelocity = new Vector3(moveInput.x * climbSpeed, moveInput.y * climbSpeed, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = Move();
        }
    }
    private Vector3 Move()
    {
        if (isRun)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
        return new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.y * currentSpeed);
    }
    private void HandleMove(Vector2 direction)
    {
        moveInput = direction;
    }
    private void HandleJump(bool value)
    {
        canJump = value;
    }
    private void HandleRun(bool value)
    {
        isRun = value;
    }
    public void Climb(bool value)
    {
        isClimb = value;
        if (value)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero; 
        }
        else
        {
            rb.useGravity = true;
        }
    }
}