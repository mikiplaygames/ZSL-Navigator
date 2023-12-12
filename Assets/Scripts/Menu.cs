using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MikiHeadDev.Core.Input;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Image image;
    private Vector2 mousePos;
    private Control control;

    private void Awake()
    {
        control = new();
        image = GetComponentInChildren<Image>();
    }
    private void OnEnable()
    {
        control.Enable();
    }
    private void OnDisable()
    {
        control.Disable();
    }
    private void Update()
    {
        mousePos = control.Player.Mouse.ReadValue<Vector2>();
        image.transform.position = -mousePos * 0.1f;
        image.transform.position += new Vector3(Screen.width/2f,Screen.height/2f,0f);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
