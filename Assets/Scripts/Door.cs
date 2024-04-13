using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IInteractable
{
    [SerializeField] private Transform doorPivot;
    Vector3 openRotation = new(0, 90, 0);
    Vector3  closeRotation;
    private bool isOpen = false;
    private void Awake()
    {
        closeRotation = doorPivot.localEulerAngles;
    }

    public void Interact()
    {
        if (LeanTween.isTweening()) return;
        ToggleDoor();
    }
    private void ToggleDoor()
    {
        if (isOpen)
            CloseDoor();
        else
            OpenDoor();
        isOpen = !isOpen;
    }

    private void OpenDoor()
    {
        if (isOpen) return;
        // Calculate the direction vector from the door to the player
        Vector3 playerDirection = (PlayerController.Instance.transform.position - transform.position).normalized;

        // Calculate the dot product to determine if the player is in front or behind the door
        float dotProduct = Vector3.Dot(transform.forward, playerDirection);

        // Determine the open rotation based on whether the player is in front or behind the door
        Vector3 targetRotation = dotProduct >= 0 ? openRotation : -openRotation;

        // Rotate the door to the open position
        LeanTween.rotateLocal(doorPivot.gameObject, targetRotation, 1f)
            .setEase(LeanTweenType.easeOutElastic);
    }

    private void CloseDoor()
    {
        if (!isOpen) return;
        // Rotate the door back to the closed position
        LeanTween.rotateLocal(doorPivot.gameObject, closeRotation, 1f)
            .setEase(LeanTweenType.easeOutElastic);
    }
}
