using System;
using UnityEngine;

public class RoomNumber : MonoBehaviour// , IInteractable
{
    [SerializeField] private string id;
    private void Awake()
    {
        id ??= name;
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
