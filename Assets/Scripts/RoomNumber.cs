using System;
using TMPro;
using UnityEngine;

public class RoomNumber : MonoBehaviour// , IInteractable
{
    [SerializeField] private string id;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        if (string.IsNullOrEmpty(id))
            id = name;
        textMeshPro.SetText(id);
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
