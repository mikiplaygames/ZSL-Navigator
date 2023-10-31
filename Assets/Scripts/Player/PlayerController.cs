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
    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera cam;
    
    private Control control;
    private CharacterController characterController;
    private CinemachineCollisionImpulseSource impulse;
    
    private Vector3 velocity;
    private Vector2 move;
    private float gravity = -9.81f;
    private bool isGrounded;

    public float distanceToGround = 0.4f;

    void Awake()
    {
        control = new();
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        impulse = GetComponentInChildren<CinemachineCollisionImpulseSource>();
    }
    private void OnEnable()
    {
        control.Enable();
        control.Player.Move.performed += MoveChanged;
        control.Player.Jump.performed += Jump;
    }
    private void MoveChanged(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            impulse.GenerateImpulse();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private void OnDisable()
    {
        control.Player.Move.performed -= MoveChanged;
        control.Player.Jump.performed -= Jump;
        control.Disable();
    }
    void FixedUpdate()
    {
        Gravity();
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        Vector3 movement = (move.y * cam.transform.forward) + (move.x * cam.transform.right);
        characterController.Move(movement * (moveSpeed * Time.deltaTime));
    }
    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
