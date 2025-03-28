using UnityEngine;
using UnityEngine.UI;

public class Shmoovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float chargeMoveSpeed = 1f;
    public float minDashSpeed = 15f;
    public float maxDashSpeed = 35f;
    public float dashDuration = 0.15f;
    public float maxChargeTime = 1.5f;
    public float dashCooldown = 0.5f;
    public float rotationSpeed = 5f; // Smooth turning speed

    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 moveDirection;
    private bool isDashing = false;
    private bool isCharging = false;
    private bool isCooldown = false;
    private float chargeTime = 0f;
    private float dashEndTime;
    private float dashCooldownTimer = 0f;

    private GameObject cooldownBar;
    private Image cooldownFill;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = 0f;

        // Setup cooldown UI
        SetupCooldownIndicator();
    }

    void Update()
    {
        HandleInput();
        HandleCharge();
        UpdateCooldown();
        RotateTowardsMovement();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (Time.time >= dashEndTime)
            {
                isDashing = false;
                col.enabled = true;
                rb.linearVelocity = Vector2.zero;
                StartCooldown();
            }
        }
        else
        {
            float speed = isCharging ? chargeMoveSpeed : moveSpeed;
            rb.linearVelocity = moveDirection * speed;
        }
    }

    void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void HandleCharge()
    {
        if (isCooldown) return; // Prevent dashing during cooldown

        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            isCharging = false;
            isDashing = true;
            isCooldown = true;
            col.enabled = false;

            float dashSpeed = Mathf.Lerp(minDashSpeed, maxDashSpeed, chargeTime / maxChargeTime);
            rb.linearVelocity = moveDirection * dashSpeed;
            dashEndTime = Time.time + dashDuration;
            chargeTime = 0f;
        }
    }

    void RotateTowardsMovement()
    {
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void StartCooldown()
    {
        isCooldown = true;
        dashCooldownTimer = dashCooldown;
    }

    void UpdateCooldown()
    {
        if (isCooldown)
        {
            dashCooldownTimer -= Time.deltaTime;
            cooldownFill.fillAmount = 1 - (dashCooldownTimer / dashCooldown);
            cooldownFill.color = Color.red; // On cooldown

            if (dashCooldownTimer <= 0)
            {
                isCooldown = false;
                cooldownFill.fillAmount = 1;
                cooldownFill.color = Color.green; // Ready to dash
            }
        }
    }

    void SetupCooldownIndicator()
    {
        // Create UI Canvas
        GameObject canvasObj = new GameObject("DashCooldownCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 100;
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create cooldown bar background
        cooldownBar = new GameObject("DashCooldownBar");
        cooldownBar.transform.SetParent(canvasObj.transform);
        Image bg = cooldownBar.AddComponent<Image>();
        bg.color = Color.black;
        RectTransform bgRect = cooldownBar.GetComponent<RectTransform>();
        bgRect.sizeDelta = new Vector2(0.3f, 0.03f);
        bgRect.localPosition = new Vector3(0, -0.8f, 0); // Lower position

        // Create cooldown fill bar
        GameObject fill = new GameObject("CooldownFill");
        fill.transform.SetParent(cooldownBar.transform);
        cooldownFill = fill.AddComponent<Image>();
        cooldownFill.color = Color.green; // Starts green
        RectTransform fillRect = fill.GetComponent<RectTransform>();
        fillRect.sizeDelta = new Vector2(0.3f, 0.03f);
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.pivot = new Vector2(0, 0.5f);
        cooldownFill.fillAmount = 1;

        // Attach canvas to the player
        canvasObj.transform.SetParent(transform);
        canvasObj.transform.localPosition = Vector3.zero;
    }
}