using MikiHeadDev.Core.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Control control;
    private bool toggled = false;

    private void Awake()
    {
        control = new();
    }
    private void OnEnable()
    {
        control.Enable();
        control.UI.Pause.performed += Toggle;
    }
    private void Toggle(InputAction.CallbackContext obj)
    {
        toggled = !toggled;
        panel.SetActive(toggled);

    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
