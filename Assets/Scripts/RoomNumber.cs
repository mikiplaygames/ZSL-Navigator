using UnityEngine;

public class RoomNumber : MonoBehaviour , IInteractable
{
    [SerializeField] private int id;
    private void Awake()
    {
        if (id == default)
            int.TryParse(name, out id);
    }
    public void Interact()
    {
        TimeTableFetcher.Instance.Hide();
        TimeTableFetcher.Instance.Reload(id);
    }
}
