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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);
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