using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Bull : MonoBehaviour
{
    [SerializeField] private float bullSpeed = 5f;
    [SerializeField] private float sleepTime = 10f;
    [SerializeField] private List<Transform> bullPoints;

    private Rigidbody RBody { get; set; }
    private NavMeshAgent Agent { get; set; }
    private Vector3 StartPosition { get; set; }

    private void Start()
    {
        if (!RBody)
            RBody = GetComponent<Rigidbody>();
        if (!Agent)
            Agent = GetComponent<NavMeshAgent>();
     
        StartPosition = this.transform.position;
        StartCoroutine(AwakenBull());
    }

    private IEnumerator AwakenBull()
    {
        Vector3 newPoint = bullPoints[Random.Range(0, bullPoints.Count)].position;
        yield return new WaitForSeconds(sleepTime);
        Run(newPoint);
        StartCoroutine(AwakenBull());
    }

    private void Run(Vector3 destination)
    {
        Agent.speed = bullSpeed;

        if (Agent)
            if (Agent.isOnNavMesh)
                Agent.SetDestination(destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        ITargetable targetable = other.GetComponent<ITargetable>();
        if (targetable != null)
        {
            targetable.RunAway(bullPoints[Random.Range(0, bullPoints.Count)]);
            return;
        }
    }
}

