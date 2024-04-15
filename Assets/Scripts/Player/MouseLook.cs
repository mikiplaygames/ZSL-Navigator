using System.Collections;
using System.Collections.Generic;
using MikiHeadDev.Core.Input;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private Vector2 mouseLook;
    private Control control;
    private float rotationX;

    void Awake()
    {
        control = new();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        if (PauseMenu.toggled) return;
        Look();
    }
    private void Look()
    {
        mouseLook = control.Player.Look.ReadValue<Vector2>();
        
        rotationX -= mouseLook.y * GameSettings.Instance.mouseSensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -90f, 90);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * (mouseLook.x * GameSettings.Instance.mouseSensitivity * Time.deltaTime));
    }
    private void OnEnable()
    {
        control.Enable();
    }
    private void OnDisable()
    {
        control.Disable();
    }
}