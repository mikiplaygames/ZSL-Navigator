using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IInteractable
{
    [SerializeField] private Transform doorPivot;
    private int _finalAngle = 0;
    private float startingAngle;
    private int finalAngle
    {
        get => _finalAngle;
        set
        {
            _finalAngle = value;
            //StartCoroutine(RotateDoor());
            LeanTween.rotateY(doorPivot.gameObject, _finalAngle, 1.5f).setEase(LeanTweenType.easeOutElastic);
            Debug.Log(_finalAngle);
        }
    }
    private void Awake()
    {
        startingAngle = doorPivot.rotation.eulerAngles.y;
    }
    public void Interact()
    {
        if (finalAngle != 180)
            finalAngle = 180;
        else
            finalAngle = 90 * Mathf.CeilToInt(
                Mathf.Clamp(PlayerController.Instance.transform.position.z - transform.position.z, -1, 1)
            );
    }
    private IEnumerator RotateDoor()
    {
        var enumeratorStartWithAngle = finalAngle;
        var startAngle = doorPivot.rotation.eulerAngles.y;
        var endAngle = startingAngle + finalAngle;
        var t = 0f;
        while (t < 1 && enumeratorStartWithAngle == finalAngle)
        {
            t += Time.deltaTime;
            doorPivot.rotation = Quaternion.Euler(0, Mathf.Lerp(startAngle, endAngle, t), 0);
            yield return null;
        }
    }
}
