// <copyright file="BomberMan.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using Humans;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PatrolPoints))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class BomberMan : MonoBehaviour, ITargetable
{
    [SerializeField] private float rotationAndLerpSpeed = 5f;
    [SerializeField] private GameObject bomb;
    private PatrolPoints PatrolPositions { get; set; }

    private Rigidbody RBody { get; set; }

    private NavMeshAgent Agent { get; set; }

    private Transform Destination { get; set; }

    public HumanType HumanType = HumanType.Normal;

    private void Start()
    {
        if (!RBody)
            RBody = GetComponent<Rigidbody>();
        if (!PatrolPositions)
            PatrolPositions = GetComponent<PatrolPoints>();
        if (!Agent)
            Agent = GetComponent<NavMeshAgent>();
    }
    private IEnumerator ActivateBomb(Transform position)
    {
        yield return new WaitForEndOfFrame();
        GameObject explosion = Instantiate(bomb, position);
    }

    private void Update()
    {
        try
        {
            if (Agent.enabled)
                if (Agent.isOnNavMesh)
                {
                    Vector3 newPos = PatrolPositions.PatrolSpots[Random.Range(0, PatrolPositions.PatrolSpots.Count)].position;
                    Agent.SetDestination(new Vector3(newPos.x,0,newPos.z));
                }
        }
        catch
        {

        }
    }

    public void BeemHuman(Transform position, float speed)
    {
        transform.LerpTransform(this, position.position, speed);
        StartCoroutine(ActivateBomb(position));
    }


    public void Lock()
    {
        RBody.constraints = RigidbodyConstraints.FreezeAll;
        Agent.enabled = false;
    }

    public void UnLock()
    {
        RBody.constraints = RigidbodyConstraints.None;
        Agent.enabled = true;
    }

    public void Remove()
    {
        Globals.OnLivesLostEvent();
        gameObject.SetActive(false);
    }


    public HumanType GetHumanType()
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetTargetImage()
    {
        throw new System.NotImplementedException();
    }

    public void SetHumanType(HumanType humanType)
    {
        HumanType = humanType;
    }

    public void SetPatrolPoints(List<Transform> patrolPoints)
    {
        if(PatrolPositions == null)
        {
            PatrolPositions = GetComponent<PatrolPoints>();
        }
        PatrolPositions.PatrolSpots = patrolPoints;
    }
}
