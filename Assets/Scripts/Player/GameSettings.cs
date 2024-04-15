using UnityEngine;
using UnityEngine.Events;

public class GameSettings : Singleton<GameSettings>
{
    private bool narrator;
    public bool Narrator {
        set { 
            narrator = value;
            OnNarratorChanged?.Invoke();
        }
        get => narrator;
    }
    public UnityEvent OnNarratorChanged = new();
    private float _mouseSensitivity = 25f;
    public float mouseSensitivity
    {
        set
        {
            _mouseSensitivity = value;
            OnMouseSensitivityChanged?.Invoke();
        }
        get => _mouseSensitivity;
    }
    public UnityEvent OnMouseSensitivityChanged = new();
    private void OnEnable()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivityX", 25f);
        OnMouseSensitivityChanged?.Invoke();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MouseSensitivityX", mouseSensitivity);
        PlayerPrefs.SetFloat("MouseSensitivityY", mouseSensitivity);
        PlayerPrefs.Save();
    }
}
