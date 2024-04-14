using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGuide : MonoBehaviour
{
    public static PlayerGuide Instance { get; private set; }
    
    [SerializeField] private LineRenderer lineRenderer;

    private NavMeshAgent agent;
    private PlayerController playerController;
    
    private Vector3 destination => Navigator.Instance.GetSelectedDestination(TimeTableFetcher.Instance.SelectedLesson);
    public bool isNavigating => !agent.isStopped;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
        if (Instance == null)
            Instance = this;
    }
    private void DisplayLines()
    {
        var path = agent.path.corners;
        lineRenderer.positionCount = path.Length;
        lineRenderer.SetPositions(path);
    }
    public void Guide()
    {
        if (destination == Vector3.zero) return;
        playerController.enabled = false;
        agent.SetDestination(destination);
        agent.isStopped = false;
        StartCoroutine(WaitForArrival());
    }
    public void Navigate()
    {
        if (destination == Vector3.zero) return;
        playerController.enabled = true;
        agent.SetDestination(destination);
        agent.isStopped = true;
        StartCoroutine(RefreshPath());
        StartCoroutine(WaitForArrival(0.5f));
    }
    private IEnumerator WaitForArrival(float distance = 0.02f)
    {
        while (agent.remainingDistance > distance)
        {
            yield return null;
        }
        playerController.enabled = true;
        agent.isStopped = true;
    }
    private IEnumerator RefreshPath()
    {
        while (!agent.isStopped && agent.hasPath)
        {
            DisplayLines();
            yield return null;
        }
    }
}
