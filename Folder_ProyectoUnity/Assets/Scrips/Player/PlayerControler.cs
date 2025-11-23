using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("HealthPoints")]
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private GameObject _deathEffect, hitEffect;
    private float _currentHealth;

    [SerializeField] private Healthbar _healthbar;

    [Header("Movement Player")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    protected bool isRun;
    private float currentSpeed;
    protected Vector2 moveInput;

    [Header("Jump Player")]
    [SerializeField] private int jumpForce;
    [SerializeField] private int maxJump; 
    private int jumpCount;
    private Rigidbody rb;
    protected bool canJump;

    [Header("Climb Player")]
    [SerializeField] private float climbSpeed;
    
    private bool isClimb;
    
    [Header("Raycast")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentHealth = _maxHealth;
        _healthbar.UpdateHealthbar(_maxHealth, _currentHealth);
    }

    private void FixedUpdate()
    {
        //LOS PODEROSOS RAYCAST
        #region Jump
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, distance, layer);

        if (isGrounded)
        { 
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.green);
        }

        if (isGrounded == true)
        {
            jumpCount = 0;
        }

        if (canJump)
        {
            if (isGrounded || jumpCount < maxJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
                canJump = false;
            }
        }
        #endregion

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
        currentSpeed = isRun ? runSpeed : speed;
        return new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.y * currentSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Jugador {gameObject.name} moviéndose: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            canJump = true;
        Debug.Log($"Jugador {gameObject.name} saltando");
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.performed;
        Debug.Log($"Jugador {gameObject.name} corriendo: {isRun}");
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

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        if (_healthbar != null)
            _healthbar.UpdateHealthbar(_maxHealth, _currentHealth);

        if (_currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        if (_deathEffect != null)
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
