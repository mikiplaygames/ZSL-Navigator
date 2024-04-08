using MikiHeadDev.Core.Input;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    private Control control;

    private void Awake()
    {
        control = new Control();
        control.Player.Interact.performed += ctx => Interact();
    }
    private void OnEnable()
    {
        control.Enable();
    }
    private void OnDisable()
    {
        control.Disable();
    }
    private void Interact()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 105, interactLayer)) return;
        var interactable = hit.transform.GetComponentInChildren<IInteractable>(true);
        interactable.Interact();
    }
}
