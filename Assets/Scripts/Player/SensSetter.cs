using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensSetter : MonoBehaviour
{
    CinemachineVirtualCamera vc;
    void Awake() 
    {
        vc = GetComponent<CinemachineVirtualCamera>();
        GameSettings.Instance.OnMouseSensitivityChanged.AddListener(SetSens);
    }
    public void SetSens()
    {
        var sensX = GameSettings.Instance.MouseSensitivity.x;
        var sensY = GameSettings.Instance.MouseSensitivity.y;
        vc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensX;
        vc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensY;
    }
}
