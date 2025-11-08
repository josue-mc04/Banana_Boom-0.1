using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement Player")]
    [SerializeField] private float speed;
    private Vector2 moveInput;

    [Header("Jump Player")]
    [SerializeField] private int jumpForce;
    private Rigidbody rb;
    private bool canJump = true;

    [Header("Rotacion de camara pal player dx")]
    public Transform camara;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //movimiento del player
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);

        //rotacion de la camara
        Vector3 rotacionCamara = camara.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotacionCamara.y, 0f);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
}