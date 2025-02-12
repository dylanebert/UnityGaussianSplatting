using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
    public float MoveSpeed = 5f;
    public float LookSensitivity = 1f;
    public Transform CameraTransform;

    private CharacterController _controller;
    private Vector2 _moveInput;
    private float _xRotation;

    private void Awake() {
        _controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector3 moveDirection = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;
        _controller.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) {
        var lookInput = context.ReadValue<Vector2>();

        const float sensitivityMultiplier = 1e-2f;
        float mouseX = lookInput.x * LookSensitivity * sensitivityMultiplier;
        float mouseY = lookInput.y * LookSensitivity * sensitivityMultiplier;

        transform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        CameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
