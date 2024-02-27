using Cinemachine;
using MikiHeadDev.Core.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private CinemachineInputProvider cameraInput;
    private Control control;
    public static bool toggled = false;

    private void Awake()
    {
        control = new();
    }
    private void OnEnable()
    {
        control.Enable();
        control.UI.Pause.performed += Toggle;
    }
    private void OnDisable()
    {
        control.Disable();
        control.UI.Pause.performed -= Toggle;
    }
    private void Toggle(InputAction.CallbackContext obj)
    {
        cameraInput.enabled = toggled;
        toggled = !toggled;
        panel.SetActive(toggled);
        if (toggled)
        {
            Cursor.lockState = CursorLockMode.None;
            panel.transform.position = new Vector3(0, panel.transform.position.y, 0);
            LeanTween.moveX(panel, 113f, 0.5f).setEase(LeanTweenType.easeOutElastic);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
