using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MikiHeadDev.Core.Input;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private Image image;
    private Vector2 mousePos;
    private Control control;
    TextToSpeech speech;
    private GameObject[] speakable;
    [SerializeField] private Toggle narratorToggle;

    private void Awake()
    {
        control = new();
        image = GetComponentInChildren<Image>();
        speech = new TextToSpeech();
        speakable = GameObject.FindGameObjectsWithTag("Speakable");
        if (GameSettings.Narrator)
        {
            narratorToggle.isOn = true;
            speech.SpeakItems(speakable);
        }

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
    public void OnNarratorToggle(bool value)
    {
        GameSettings.Narrator = value;
        if (value)
            speech.SpeakItems(speakable);
    }
}
