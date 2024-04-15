using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MikiHeadDev.Core.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float jumpHeight = 2.4f;
    [SerializeField] private float accelerationStep = 0.05f;
    [SerializeField] private float deccelerationStep = 0.2f;
    [SerializeField] private Transform ground;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask playerLayer;
    
    private Control control;
    private CharacterController characterController;
    
    private Vector3 velocity;
    private Vector2 move;
    private Vector2 moveInput;
    private float speed;
    private float run;
    private float gravity = -9.81f;
    private bool isGrounded;
    private float acceleration = 0f;
    private float moveNoise = 1f;

    public float distanceToGround = 0.4f;
    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        speed = moveSpeed;
        control = new();
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }
    private void OnEnable()
    {
        control.Enable();
        control.Player.Move.performed += MoveChanged;
        control.Player.Jump.performed += Jump;
        control.Player.Run.performed += RunStart;
        control.Player.Run.canceled += RunStop;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        control.Player.Move.performed -= MoveChanged;
        control.Player.Jump.performed -= Jump;
        control.Player.Run.performed -= RunStart;
        control.Player.Run.canceled -= RunStop;
        control.Disable();
    }
    private void MoveChanged(InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void RunStart(InputAction.CallbackContext obj)
    {
        speed = runSpeed;
        moveNoise = 2f;
    }
    private void RunStop(InputAction.CallbackContext obj)
    {
        speed = moveSpeed;
        moveNoise = 1f;
    }
    private void FixedUpdate()
    {
        Gravity();
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        if (!moveInput.Equals(Vector3.zero)) move = moveInput;
        Vector3 movement = (move.y * new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized) + (move.x * cam.transform.right);
        acceleration = !moveInput.Equals(Vector3.zero) ? Mathf.Clamp(acceleration + accelerationStep, 0, 1f) : Mathf.Lerp(acceleration, 0, deccelerationStep);
        characterController.Move(movement * (speed * Time.deltaTime) * acceleration);
    }
    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, playerLayer);
        
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
