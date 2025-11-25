using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour, IThrowAble
{
    [Header("References")]
    public Transform cameraTransform; //aquí va la MainCamera

    [Header("HealthPoints")]
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private GameObject _deathEffect, hitEffect;
    private float _currentHealth;
    [SerializeField] private Healthbar _healthbar;

    [Header("Movement Player")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    protected bool isRun;
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


    public bool isKnockback;
    public float knockbackDuration;
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
        #region Move
        Vector3 direccion = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        float velocidadActual = isRun ? runSpeed : speed;


        Vector3 localDir = transform.TransformDirection(direccion);
        localDir *= speed;  

        Vector3 currentDir = rb.linearVelocity;

        localDir.y = currentDir.y;  

        if(isKnockback == false)
            rb.linearVelocity = localDir;

        Debug.Log("Magnitud de la direccion = " + direccion.magnitude);
        #endregion

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
        /*
        #region Climb
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
        #endregion*/


        transform.forward = GetLookDir();
    }

    public void Throw()
    {
        isKnockback = true;
        Invoke("DisableKnockback", knockbackDuration);
        //corrutina apra desactivar el isknoca
    }
    public void DisableKnockback()
    {
        isKnockback = false;
    }
    public Vector3 GetLookDir()
    {
        Vector3 LookDir = (transform.position- cameraTransform.position).normalized;

        LookDir.y = 0;

        return LookDir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Jugador {gameObject.name} moviendose: {moveInput}");
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