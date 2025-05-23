using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Componentes")]
    public Camera playerCamera;
    private Rigidbody rb;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    private float cameraVerticalAngle = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LookAround();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * inputX + transform.forward * inputZ;
        Vector3 moveVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        rb.linearVelocity = moveVelocity;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotar cuerpo en Y
        transform.Rotate(Vector3.up * mouseX);

        // Rotar c�mara en X
        cameraVerticalAngle -= mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Considera al personaje en el suelo si toca algo etiquetado como "Ground"
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
