using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;
    private CharacterController controller;

    [Header("Movement Speeds")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float rotationSpeed = 10f;

    [Header("Jumping & Gravity")]
    public float jumpHeight = 2f;
    public float gravity = -19.62f;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Jumping Fixes")]
    [Tooltip("Grace period in seconds allowing you to jump right after leaving an edge or slope.")]
    public float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Orbit Camera Settings")]
    public float mouseSensitivity = 3f;
    public float cameraDistance = 5f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 65f;

    private float cameraX = 0f;
    private float cameraY = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // --- COYOTE TIME LOGIC ---
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset timer when firmly on ground

            if (playerVelocity.y < 0)
            {
                playerVelocity.y = -2f; // Keep pinned to slopes
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Count down when airborne
        }

        HandleCameraOrbit();
        HandleMovement();

        // --- FIXED JUMP CHECK ---
        // Instead of checking isGrounded, check if our grace timer is active
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimeCounter = 0f; // Instantly expend the timer so they can't double jump
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleCameraOrbit()
    {
        if (playerCamera == null) return;

        cameraX += Input.GetAxis("Mouse X") * mouseSensitivity;
        cameraY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        cameraY = Mathf.Clamp(cameraY, minVerticalAngle, maxVerticalAngle);

        Quaternion cameraRotation = Quaternion.Euler(cameraY, cameraX, 0f);
        Vector3 targetPivot = transform.position + Vector3.up * 1f;
        Vector3 cameraPosition = targetPivot - (cameraRotation * Vector3.forward * cameraDistance);

        playerCamera.rotation = cameraRotation;
        playerCamera.position = cameraPosition;
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            Vector3 cameraForward = playerCamera.forward;
            Vector3 cameraRight = playerCamera.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
            controller.Move(moveDirection * targetSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
