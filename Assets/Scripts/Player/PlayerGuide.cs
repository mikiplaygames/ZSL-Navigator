using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGuide : MonoBehaviour
{
    public static PlayerGuide Instance { get; private set; }

    private NavMeshAgent agent;
    [SerializeField] private Transform dest;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (Instance == null)
            Instance = this;
    }
    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public void Stop()
    {
        agent.isStopped = true;
    }
    public void Resume()
    {
        agent.isStopped = false;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Stop"))
        {
            Stop();
        }
        if (GUI.Button(new Rect(10, 70, 100, 50), "Resume"))
        {
            Resume();
        }
        if (GUI.Button(new Rect(10, 130, 100, 50), "Set Destination"))
        {
            SetDestination(dest.position);
        }
    }
}
