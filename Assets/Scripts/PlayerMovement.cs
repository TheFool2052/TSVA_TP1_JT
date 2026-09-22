using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    public float mouseSensitivity = 2f;

    public Vector3 moveDirection;
    public float rotationY;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * mouseSensitivity;

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
