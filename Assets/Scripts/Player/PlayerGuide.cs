using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class PlayerGuide : MonoBehaviour
{
    public static PlayerGuide Instance { get; private set; }
    
    [SerializeField] private LineRenderer lineRenderer;

    private NavMeshAgent agent;
    private PlayerController playerController;
    
    private Vector3 destination => Navigator.Instance.GetSelectedDestination(TimeTableFetcher.Instance.SelectedLesson.Split(" ")[^1]);
    public bool isNavigating => agent.enabled && !agent.isStopped;

    private Coroutine guidence;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
        agent.isStopped = true;
        agent.enabled = false;
        if (Instance == null)
            Instance = this;
    }
    public void Guide()
    {
        if (isNavigating)
        {
            agent.isStopped = true;
            agent.enabled = false;
            playerController.enabled = true;
            StopCoroutine(guidence);
            return;
        }
        if (destination == Vector3.zero) return;
        playerController.enabled = false;
        agent.enabled = true;
        agent.SetDestination(destination);
        agent.isStopped = false;
        if (guidence != null)
            StopCoroutine(guidence);
        guidence = StartCoroutine(WaitForArrival());
    }
    public void Navigate()
    {
        if (agent.destination == destination || lineRenderer.positionCount > 0)
        {
            agent.isStopped = true;
            agent.enabled = false;
            playerController.enabled = true;
            StopCoroutine(guidence);
            lineRenderer.positionCount = 0;
            return;
        }
        if (destination == Vector3.zero) return;
        playerController.enabled = true;
        agent.enabled = true;
        agent.SetDestination(destination);
        agent.isStopped = true;
        if (guidence != null)
            StopCoroutine(guidence);
        guidence = StartCoroutine(WaitForArrival(3));
    }
    private IEnumerator WaitForArrival(float distance = 1.5f)
    {
        yield return null;
        while (Vector3.Distance(transform.position, agent.destination) > distance)//(agent.remainingDistance > distance)
        {
            yield return null;
        }
        playerController.enabled = true;
        agent.isStopped = true;
        agent.enabled = false;
    }
    private void Update()
    {
        if (agent.path.corners.Length < 2 || !agent.isStopped)
        {
            lineRenderer.positionCount = 0;
            return;
        }
        lineRenderer.positionCount = agent.path.corners.Length;
        int i = 0;
        foreach (var VARIABLE in agent.path.corners)
        {
            lineRenderer.SetPosition(i, VARIABLE + new Vector3(0,3,0));
            i++;
        }
        lineRenderer.SetPosition(0, Vector3.Lerp(transform.position, agent.path.corners[0], 0.3f) + new Vector3(0, 3, 0));
    }
}
