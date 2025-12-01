using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerControler : MonoBehaviour, IThrowAble
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
    protected bool isRun;
    protected Vector2 moveInput;

    [Header("Jump Player")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int maxJump = 2;
    private int jumpCount;
    private Rigidbody rb;
    protected bool canJump;

    [Header("Climb Player")]
    [SerializeField] private float climbSpeed;
    private bool isClimb;

    [Header("Raycast")]
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private LayerMask layer;

    [Header("Physics")]
    [SerializeField] private float normalMass = 1f;
    [SerializeField] private float airMass = 1.5f;

    public bool isKnockback;
    public float knockbackDuration;
    private bool isGrounded;

    [Header("Look / Camera")]
    [SerializeField] private float lookSensitivity = 3f;
    private Vector2 lookInput;
    private float rotX;

    [Header("Unity Events")]
    public UnityEvent<Vector2> OnMoveEvent;
    public UnityEvent OnJumpEvent;
    public UnityEvent<Vector2> OnLookEvent;
    public UnityEvent<bool> OnClimbEvent;
    public UnityEvent OnDieEvent;

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
        if (_healthbar != null)
            _healthbar.UpdateHealthbar(_maxHealth, _currentHealth);

        rb.mass = normalMass;
    }

    private void FixedUpdate()
    {
        #region Move
        if (isGrounded && !isKnockback)
        {
            Vector3 direccion = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            float velocidadActual = isRun ? runSpeed : speed;

            Vector3 localDir = transform.TransformDirection(direccion) * velocidadActual;
            Vector3 currentVelocity = rb.linearVelocity;
            localDir.y = currentVelocity.y;
            rb.linearVelocity = localDir;
        }
        else if (!isKnockback)
        {
            Vector3 direccion = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Vector3 localDir = transform.TransformDirection(direccion) * (speed * 0.5f);
            rb.AddForce(localDir * 10f, ForceMode.Force);
        }
        #endregion

        #region Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distance, layer);
        Debug.DrawRay(transform.position, Vector3.down * distance, isGrounded ? Color.red : Color.green);

        rb.mass = isGrounded ? normalMass : airMass;

        if (isGrounded)
            jumpCount = 0;
        #endregion

        #region Jump
        if (canJump)
        {
            if (isGrounded || jumpCount < maxJump)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                jumpCount++;
                canJump = false;
            }
        }
        #endregion

        if (rb.linearVelocity.y < 0)
            rb.linearVelocity += new Vector3(0, -25.0f, 0) * Time.deltaTime;
    }

    private void LateUpdate()
    {
        // Rotación cámara (pitch)
        rotX -= lookInput.y * lookSensitivity;
        rotX = Mathf.Clamp(rotX, -80f, 80f);
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);

        // Rotación jugador (yaw)
        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);
    }

    // ---------------------- INPUTS ----------------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canJump = true;
            OnJumpEvent?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        OnLookEvent?.Invoke(lookInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.performed;
    }

    // ---------------------- CLIMB ----------------------
    public void Climb(bool value)
    {
        isClimb = value;
        rb.useGravity = !value;
        if (value)
            rb.linearVelocity = Vector3.zero;

        OnClimbEvent?.Invoke(value);
    }

    // ---------------------- VIDA ----------------------
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

        OnDieEvent?.Invoke();
        Destroy(gameObject);
    }

    // ---------------------- KNOCKBACK ----------------------
    public void Throw()
    {
        isKnockback = true;
        Invoke("DisableKnockback", knockbackDuration);
    }

    public void DisableKnockback()
    {
        isKnockback = false;
    }
}
