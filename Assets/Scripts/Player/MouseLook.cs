using System.Collections;
using System.Collections.Generic;
using MikiHeadDev.Core.Input;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private Vector2 mouseLook;
    private Control control;
    private float rotationX = 0f;

    void Awake()
    {
        control = new();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Look();
    }
    private void Look()
    {
        mouseLook = control.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * GameSettings.MouseSensitivity.x * Time.deltaTime;
        float mouseY = mouseLook.y * GameSettings.MouseSensitivity.y * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void OnEnable()
    {
        control.Enable();
        Debug.Log(GameSettings.MouseSensitivity);

    }
    private void OnDisable()
    {
        control.Disable();
    }
}
