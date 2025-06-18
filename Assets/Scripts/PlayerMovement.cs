using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Movement Components
    private PlayerInput playerInput;
    private Rigidbody rb;

    // Speed Settings
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float abilitySpeed = 10f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float minFishingSpeed = 1f;

    // Movement Variables
    private Vector2 movementInput;
    private Vector3 currentMovement, toIso;
    private bool isMovementPressed;

    // Boost System
    [Header("Boost Settings")]
    [SerializeField] private float abilityDuration = 6f;
    [SerializeField] private float cooldownDuration = 11f;
    [SerializeField] private GameObject smokeEffect;
    private float abilityTimer = 0f;
    private float cooldownTimer = 0f;
    private bool isAbilityActive = false;
    private bool isOnCooldown = false;

    public Slider abilitySlider;

    // Fishing Reference
    private Fishing fishingScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        fishingScript = GetComponent<Fishing>();
        playerInput = new PlayerInput();
        rb.maxLinearVelocity = abilitySpeed;

        // Input Setup
        playerInput.PlayerController.Movement.started += OnMove;
        playerInput.PlayerController.Movement.performed += OnMove;
        playerInput.PlayerController.Movement.canceled += OnMove;
        playerInput.PlayerController.Fast.started += OnFast;
        playerInput.PlayerController.Fast.canceled += OnFast;
    }

    private void Start()
    {
        if (smokeEffect != null) smokeEffect.SetActive(false);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
        currentMovement = new Vector3(movementInput.x, 0, movementInput.y);
        isMovementPressed = movementInput != Vector2.zero;
    }

    private void OnFast(InputAction.CallbackContext context)
    {
        if (context.started && !isAbilityActive && !isOnCooldown)
        {
            StartBoost();
        }
    }

    private void StartBoost()
    {
        abilitySlider.maxValue = abilityDuration;
        isAbilityActive = true;
        abilityTimer = abilityDuration;

        if (isMovementPressed)
        {
            rb.velocity = toIso.normalized * abilitySpeed;
        }

        if (fishingScript != null)
        {
            fishingScript.SetFishingSpeed(true);
        }

        if (smokeEffect != null)
        {
            smokeEffect.SetActive(true);
        }

        Invoke("EndBoost", abilityDuration);
    }

    private void EndBoost()
    {
        if (!isAbilityActive) return;

        isAbilityActive = false;

        if (smokeEffect != null) smokeEffect.SetActive(false);

        if (isMovementPressed)
        {
            rb.velocity = toIso.normalized * normalSpeed;
        }

        if (fishingScript != null)
        {
            fishingScript.SetFishingSpeed(false);
        }

        isOnCooldown = true;
        cooldownTimer = cooldownDuration;

        abilitySlider.maxValue = cooldownDuration;
    }

    private void Update()
    {
        // Boost timer
        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;

            abilitySlider.value = abilityTimer;

            if (abilityTimer <= 0f)
            {
                EndBoost();
            }
        }

        // Cooldown timer
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            abilitySlider.value += Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }

        // Fishing control based on speed
        if (fishingScript != null)
        {
            bool isMovingFastEnough = rb.velocity.magnitude > minFishingSpeed;
            fishingScript.CheckInactivity(isMovingFastEnough);
        }
    }

    private void FixedUpdate()
    {
        if (isMovementPressed)
        {
            RotationProcess();
            PlayerMove();

            // Boost aktifken ekstra hýz kontrolü
            if (isAbilityActive)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, abilitySpeed);
            }
        }
    }

    void PlayerMove()
    {
        float currentSpeed = isAbilityActive ? abilitySpeed : normalSpeed;
        Vector3 targetVelocity = toIso * currentSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 15f);
    }

    void RotationProcess()
    {
        Quaternion isoRotation = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(isoRotation);
        toIso = isoMatrix.MultiplyPoint3x4(currentMovement).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(toIso, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        if (playerInput != null)
            playerInput.PlayerController.Enable();
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.PlayerController.Disable();
    }

}