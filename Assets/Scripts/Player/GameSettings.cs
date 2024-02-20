using System.Collections;
using System.Collections.Generic;
using MikiHeadDev.Core.Input;
using UnityEngine;
using UnityEngine.Events;

public class GameSettings : MonoBehaviour
{
    private static bool narrator = false;
    public static bool Narrator {
        set { 
            narrator = value;
            OnNarratorChanged?.Invoke();
        }
        get { return narrator; }
    }
    public static UnityEvent OnNarratorChanged = new();
    public static Vector2 MouseSensitivity;
    public static UnityEvent OnMouseSensitivityChanged = new();
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
    public void ChangeX(float value)
    {
        MouseSensitivity.x = value;
        OnMouseSensitivityChanged?.Invoke();
    }
    public void ChangeY(float value)
    {
        MouseSensitivity.y = value;
        OnMouseSensitivityChanged?.Invoke();
    }
}
