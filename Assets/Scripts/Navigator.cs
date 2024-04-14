using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public static Navigator Instance { get; private set; }
    private Dictionary<string, Vector3> rooms;
    private void Awake()
    {
        rooms = new();
        if (Instance == null)
            Instance = this;
    }
    public void AddClass(string id, Vector3 position)
    {
        rooms.Add(id, position);
    }
    public Vector3 GetSelectedDestination(string id) => rooms[id];
}
