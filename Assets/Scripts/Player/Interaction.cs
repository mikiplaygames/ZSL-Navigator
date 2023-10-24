using MikiHeadDev.Core.Input;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    private Control control;
    [SerializeField] private TimeTableFetcher timeTableFetcher;

    private void Awake()
    {
        control = new Control();
    }

    private void OnEnable()
    {
        control.Enable();
        control.Player.Interact.performed += ctx => Interact();
    }

    private void OnDisable()
    {
        control.Disable();
        control.Player.Interact.performed -= ctx => Interact();
    }

    private void Interact()
    {
        timeTableFetcher.Hide();
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 105, interactLayer))
        {
            int.TryParse(hit.transform.name, out var id);
            timeTableFetcher.Reload(id);
        }
    }
}
