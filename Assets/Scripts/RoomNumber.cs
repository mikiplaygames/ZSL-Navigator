using System;
using UnityEngine;

public class RoomNumber : MonoBehaviour , IInteractable
{
    [SerializeField] private int id;
    private void Awake()
    {
        if (id == default)
            int.TryParse(name, out id);
    }

    private void Start()
    {
        //todo KlasaDupa.Instance.AddRoom(this); i na tej podstawie sie on bedzie wiedzial z czego wybierac
    }

    public void Interact()
    {
        TimeTableFetcher.Instance.Hide();
        TimeTableFetcher.Instance.DisplayTimeTable(id);
    }
}
