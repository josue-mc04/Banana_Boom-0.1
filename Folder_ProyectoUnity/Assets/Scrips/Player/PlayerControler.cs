using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform; // ← aquí va la MainCamera

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

    protected bool isRun;
    private bool isClimb;

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
            rb.linearVelocity = new Vector3(
                moveInput.x * climbSpeed,
                moveInput.y * climbSpeed,
                rb.linearVelocity.z
            );
        }
        else
        {
            rb.linearVelocity = MoveRelativeToCamera();
        }
    }

    private Vector3 MoveRelativeToCamera()
    {
        currentSpeed = isRun ? runSpeed : speed;

        if (cameraTransform == null)
            return new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.y * currentSpeed);

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredDirection =
            camForward * moveInput.y +
            camRight * moveInput.x;

        // Rotar player hacia donde se mueve (en base a cámara)
        if (desiredDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 15f * Time.deltaTime);
        }

        desiredDirection *= currentSpeed;
        desiredDirection.y = rb.linearVelocity.y;

        return desiredDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            canJump = true;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.performed;
    }

    public void Climb(bool value)
    {
        isClimb = value;
        rb.useGravity = !value;

        if (value)
            rb.linearVelocity = Vector3.zero;
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