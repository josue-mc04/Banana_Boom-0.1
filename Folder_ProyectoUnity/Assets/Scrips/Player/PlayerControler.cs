using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerControler : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("HealthPoints")]
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private GameObject _deathEffect, hitEffect;
    private float _currentHealth;
    [SerializeField] private Healthbar _healthbar;

    [Header("Movement Player")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;
    private bool isRun;
    private Vector2 moveInput;

    [Header("Jump Player")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int maxJump = 2;
    private int jumpCount;
    private Rigidbody rb;
    private bool canJump;

    [Header("Raycast Ground Check")]
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private LayerMask layer;

    [Header("Physics")]
    [SerializeField] private float normalMass = 1f;
    [SerializeField] private float airMass = 1.5f;

    private bool isKnockback;
    public float knockbackDuration;

    private bool isGrounded;
    private RaycastHit hit;

    [Header("Look / Camera")]
    [SerializeField] private float lookSensitivity = 3f;
    private Vector2 lookInput;
    private float rotX;

    [Header("Unity Event")]
    public UnityEvent OnDieEvent; // ÚNICO EVENTO ACTIVO

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (cameraTransform == null)
        {
            Camera cam = Camera.main;
            if (cam != null)
                cameraTransform = cam.transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentHealth = _maxHealth;
        _healthbar?.UpdateHealthbar(_maxHealth, _currentHealth);

        rb.mass = normalMass;
    }

    private void FixedUpdate()
    {
        GroundCheck();

        if (!isKnockback)
            MovePlayer();

        HandleJump();

        if (rb.linearVelocity.y < 0)
            rb.AddForce(Vector3.down * 25f * Time.deltaTime, ForceMode.Acceleration);
    }

    private void LateUpdate()
    {
        rotX -= lookInput.y * lookSensitivity;
        rotX = Mathf.Clamp(rotX, -80f, 80f);

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);

        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);
    }

    // ---------------------- MOVEMENT ----------------------
    void MovePlayer()
    {
        Vector3 inputDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (inputDir.magnitude > 0.1f)
        {
            float currentSpeed = isRun ? runSpeed : speed;
            Vector3 worldDir = transform.TransformDirection(inputDir) * currentSpeed;

            rb.linearVelocity = new Vector3(worldDir.x, rb.linearVelocity.y, worldDir.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    // ---------------------- GROUND CHECK ----------------------
    void GroundCheck()
    {
        isGrounded = false;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance, layer))
        {
            if (hit.collider.CompareTag("Jumpers"))
            {
                canJump = true;
            }
            else
            {
                isGrounded = true;
            }
        }

        rb.mass = isGrounded ? normalMass : airMass;

        if (isGrounded)
            jumpCount = 0;

        Debug.DrawRay(transform.position, Vector3.down * distance, isGrounded ? Color.green : Color.red);
    }

    // ---------------------- JUMP ----------------------
    void HandleJump()
    {
        if (canJump)
        {
            if (isGrounded || jumpCount < maxJump)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                jumpCount++;
            }

            canJump = false;
        }
    }

    // ---------------------- INPUTS ----------------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            canJump = true;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.ReadValue<float>() > 0.5f;
    }

    // ---------------------- VIDA ----------------------
    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        _healthbar?.UpdateHealthbar(_maxHealth, _currentHealth);

        if (_currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        if (_deathEffect != null)
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

        OnDieEvent?.Invoke(); // ÚNICO EVENTO
        Destroy(gameObject);
    }

    // ---------------------- KNOCKBACK ----------------------
    public void Throw()
    {
        isKnockback = true;
        Invoke(nameof(DisableKnockback), knockbackDuration);
    }

    public void DisableKnockback()
    {
        isKnockback = false;
    }
}
