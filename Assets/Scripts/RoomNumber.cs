using System;
using TMPro;
using UnityEngine;

public class RoomNumber : MonoBehaviour// , IInteractable
{
    [SerializeField] private string id;
    [SerializeField] TextMeshPro textMeshPro;
    private void Awake()
    {
        if (string.IsNullOrEmpty(id))
            id = name;
        textMeshPro.SetText(id);
        Debug.Log(id);
    }
    private void Start()
    {
        Navigator.Instance.AddClass(id, transform.position);
    }
    public void Interact()
    {
        TimeTableFetcher.Instance.Hide();
        //TimeTableFetcher.Instance.DisplayTimeTable(id);
    }
}
