using System.Collections;
using System.Collections.Generic;
using MikiHeadDev.Core.Input;
using UnityEngine;
using UnityEngine.Events;

public class GameSettings : Singleton<GameSettings>
{
    private bool narrator = false;
    public bool Narrator {
        set { 
            narrator = value;
            OnNarratorChanged?.Invoke();
        }
        get { return narrator; }
    }
    public UnityEvent OnNarratorChanged = new();
    public Vector2 MouseSensitivity;
    public UnityEvent OnMouseSensitivityChanged = new();
    private void OnEnable()
    {
        MouseSensitivity = new Vector2(PlayerPrefs.GetFloat("MouseSensitivityX", 100f), PlayerPrefs.GetFloat("MouseSensitivityY", 100f));
        OnMouseSensitivityChanged?.Invoke();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MouseSensitivityX", MouseSensitivity.x / 100f);
        PlayerPrefs.SetFloat("MouseSensitivityY", MouseSensitivity.y / 100f);
        PlayerPrefs.Save();
    }
    public void SetSensitivity(float value)
    {
        MouseSensitivity.x = value;
        MouseSensitivity.y = value;
        OnMouseSensitivityChanged?.Invoke();
    }
}
