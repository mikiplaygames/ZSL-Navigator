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
    [SerializeField] private float jumpHeight = 2.4f;
    [SerializeField] private int wobbleTreshold = 10;
    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera cam;
    
    private Control control;
    private CharacterController characterController;
    private CinemachineImpulseSource impulse;
    
    private Vector3 velocity;
    private Vector2 move;
    private float gravity = -9.81f;
    private bool isGrounded;
    private float lastY;

    public float distanceToGround = 0.4f;

    void Awake()
    {
        control = new();
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        impulse = GetComponent<CinemachineImpulseSource>();
        lastY = 0;
    }
    private void OnEnable()
    {
        control.Enable();
        control.Player.Move.performed += MoveChanged;
        control.Player.Jump.performed += Jump;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        control.Player.Move.performed -= MoveChanged;
        control.Player.Jump.performed -= Jump;
        control.Disable();
    }
    private void MoveChanged(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private void FixedUpdate()
    {
        Gravity();
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        Debug.Log(velocity.magnitude);   
        Vector3 movement = (move.y * new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized) + (move.x * cam.transform.right);
        characterController.Move(movement * (moveSpeed * Time.deltaTime));
    }
    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);
        if (isGrounded && Mathf.Abs(lastY) - Mathf.Abs(transform.position.y) > wobbleTreshold)
        {
            // impact
            impulse.GenerateImpulse();
        }
        lastY = transform.position.y;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
