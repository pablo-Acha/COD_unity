using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 100f;

    public Camera cam;
    public CharacterController controller;
    public WeaponSystem weapon;
    public GameObject crosshair; // 👈 mira UI

    private Vector2 moveInput;
    private Vector2 lookInput;

    private bool isRunning;
    private bool isAiming;

    private float yVelocity;
    private float xRotation = 0f;
    public Transform weaponHolder;

    public Vector3 normalPos;
    public Vector3 aimPos;

    public float aimSpeed = 10f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
        Aim();
        HandleWeaponPosition();
    }

    // 🎮 INPUT

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }

    void OnAim(InputValue value)
    {
        isAiming = value.isPressed;
    }

    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            weapon.Shoot();
        }
    }

    // 🚶 MOVIMIENTO

    void Move()
    {
        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * speed * Time.deltaTime);
    }

    // 🎥 ROTACIÓN FPS

    void Look()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (cuerpo)
        transform.Rotate(Vector3.up * mouseX);
    }

    // 🎯 APUNTAR + MIRA

    void Aim()
    {
        cam.fieldOfView = isAiming ? 40f : 60f;

        if (crosshair != null)
        {
            crosshair.SetActive(isAiming); // 👈 aparece solo al apuntar
        }
    }
    void HandleWeaponPosition()
    {
        Vector3 targetPos = isAiming ? aimPos : normalPos;
        Debug.Log("miraaaa");
        weaponHolder.localPosition = Vector3.Lerp(
            weaponHolder.localPosition,
            targetPos,
            Time.deltaTime * aimSpeed
        );
    }
}