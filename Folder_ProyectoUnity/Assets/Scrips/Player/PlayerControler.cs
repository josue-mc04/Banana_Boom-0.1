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
    [SerializeField] private float climbSpeed;
    private float currentSpeed;
    protected Vector2 moveInput; 

    [Header("Jump Player")]
    [SerializeField] private int jumpForce;
    private Rigidbody rb;
    protected bool canJump; 

    [Header("Raycast")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private Color Color = Color.red;

    protected bool isRun;
    private bool isClimb;

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
        if (Physics.Raycast(transform.position, Vector3.down, distance, layer))
        {
            if (canJump)
            {
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                canJump = false;
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
