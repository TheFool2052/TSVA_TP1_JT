using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    [SerializeField] public Transform camera;

    [Header("Movement Settings")]
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float sprintSpeed = 10f;
    [SerializeField] public float sprintTransitSpeed = 5f;
    [SerializeField] public float turningSpeed = 2f;
    [SerializeField] public float gravity = 9.81f;
    [SerializeField] public float jumpHeight = 2f;

    public float verticalVelocity;
    public float speed;

    [Header("Input")]
    public float moveInput;
    public float turnInput;

    public void Start()
    {
      controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        InputManagement();
        Movement();
    }

    public void Movement()
    {
        GroundMovement();
        Turn();
    }

    public void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = camera.transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        
        move *= speed;

        move.y = VerticalForceCalculation();

        controller.Move(move * Time.deltaTime);
    }

    public void Turn()
    {
        if (Mathf.Abs(turnInput) < 0 || Mathf.Abs(moveInput) > 0)
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;

            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
        
    }

    public float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    public void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
}
