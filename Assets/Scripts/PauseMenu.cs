using Cinemachine;
using MikiHeadDev.Core.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Toggle narratorToggle;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Button focusButton;
    [SerializeField] private PlayerController playerController;
    private Control control;
    // private Speaker speaker;
    public static bool toggled = false;

    private void Awake()
    {
        control = new();
        // speaker = new Speaker();
        // speaker.Speak();
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        sensitivitySlider.value = GameSettings.Instance.mouseSensitivity;
    }
    private void OnEnable()
    {
        // narratorToggle.isOn = GameSettings.Instance.Narrator;
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
        toggled = !toggled;
        
        panel.SetActive(toggled);
        if (toggled)
        {
            Cursor.lockState = CursorLockMode.None;
            panel.transform.position = new Vector3(0, panel.transform.position.y, 0);
            LeanTween.moveX(panel, 113f, 0.5f).setEase(LeanTweenType.easeOutElastic);
            focusButton.Select();
        }
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void ExitToMainMenu()
    {
        toggled = !toggled;
        SceneManager.LoadScene("MAIN MENU", LoadSceneMode.Single);
    }
    public void SetSensitivity(float value)
    {
        GameSettings.Instance.mouseSensitivity = value;
    }
    public void OnNarratorToggle(bool value)
    {
        GameSettings.Instance.Narrator = value;
        // if (value)
        //     speaker.Speak();
    }
}
