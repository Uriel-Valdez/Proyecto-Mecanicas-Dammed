using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Rotation")]
    public float rotationSensibility = 2f;

    [Header("Jump")]
    public float jumpHeight = 1.9f;

    private bool isCrouching = false;

    private float cameraVerticalAngle = 0f;
    private Vector3 moveInput = Vector3.zero;
    private CharacterController characterController;

    // 🔽 Variables nuevas para efecto de ralentización
    private float speedMultiplier = 1f;
    private float slowTimer = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // 🔽 Permitir desbloquear/mostrar cursor con ESC
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // 🔽 Volver a ocultar/bloquear cursor al hacer clic
        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // 🔽 Control de duración del efecto slow
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
                speedMultiplier = 1f;
        }

        Look();
        Move();
        /*HandleCrouch();*/
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
                moveInput = transform.TransformDirection(moveInput) * runSpeed * speedMultiplier;
            else
                moveInput = transform.TransformDirection(moveInput) * walkSpeed * speedMultiplier;

            if (Input.GetButtonDown("Jump"))
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSensibility;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSensibility;

        transform.Rotate(Vector3.up * mouseX);

        cameraVerticalAngle -= mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);
    }

    
    public void ApplySlow(float factor, float duration)
    {
        speedMultiplier = factor;
        slowTimer = duration;
        Debug.Log("Jugador ralentizado a " + (walkSpeed * factor) + " por " + duration + " segundos.");
    }
}
