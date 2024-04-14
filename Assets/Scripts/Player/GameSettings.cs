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

    public float _mouseSensitivity;
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
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivityX", 0.55f);
        OnMouseSensitivityChanged?.Invoke();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MouseSensitivityX", mouseSensitivity);
        PlayerPrefs.SetFloat("MouseSensitivityY", mouseSensitivity);
        PlayerPrefs.Save();
    }
}
