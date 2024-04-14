using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var s = new GameObject();
                _instance = s.AddComponent<T>();
                s.name = typeof(T).ToString();
            }
            return _instance;
        }
    }
    protected void Awake()
    {
        if (_instance == null) _instance = this as T;
        else if (_instance != this) Destroy(this);
    }
}
