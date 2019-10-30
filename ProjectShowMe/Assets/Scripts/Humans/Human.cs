// <copyright file="HUMAN.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Humans
{
    public enum HumanType
    {
        Normal = 35,
        Fat = 30,
        Strong = 15,
        Skinny = 8,
        Small = 6,
        Cool = 4,
        Rare = 2,
        Legendary = 1,
        Bomber = 0
    }
    [RequireComponent(typeof(PatrolPoints))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Human : MonoBehaviour, ITargetable
    {
        public HumanType HumanType = HumanType.Normal;

        [SerializeField] private float rotationAndLerpSpeed = 5f;

        private PatrolPoints PatrolPositions { get; set; }

        private Rigidbody RBody { get; set; }

        private NavMeshAgent Agent { get; set; }

        private int randomSpot = 0;
        private Vector3 newPosition = Vector3.zero;

        private bool beemReady = false;
        private Vector3 ufoPosition = Vector3.zero;
        private void Awake()
        {
            if (!RBody)
                RBody = GetComponent<Rigidbody>();
            if (!PatrolPositions)
                PatrolPositions = GetComponent<PatrolPoints>();
            if (!Agent)
                Agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            randomSpot = Random.Range(0, PatrolPositions.PatrolSpots.Count);
            newPosition = new Vector3(PatrolPositions.PatrolSpots[randomSpot].position.x, 0, PatrolPositions.PatrolSpots[randomSpot].position.z);

        }

        private void Update()
        {
            var newPointDistance = PatrolPositions.PatrolSpots[randomSpot].position - transform.position;
            var step = rotationAndLerpSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, newPointDistance, step, 0.0f);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), newPosition) > 1f)
            {
                try
                {
                    if (Agent.enabled)
                        if(Agent.isOnNavMesh)
                            Agent.SetDestination(newPosition);
                }
                catch
                {

                }
            }
            else
            {
                randomSpot = Random.Range(0, PatrolPositions.PatrolSpots.Count);
                newPosition = new Vector3(PatrolPositions.PatrolSpots[randomSpot].position.x, 0, PatrolPositions.PatrolSpots[randomSpot].position.z);
            }
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
            EventManager<GameObject>.BroadCast(EVENT.humanCollected, this.gameObject);
            transform.parent = null;
            gameObject.SetActive(false);
        }

        public HumanType GetHumanType()
        {
            return HumanType;
        }

        public void SetHumanType(HumanType humanType)
        {
            HumanType = humanType;
        }

        public void SetPatrolPoints(List<Transform> patrolPoints)
        {
            PatrolPositions.PatrolSpots = patrolPoints;
        }

        public Sprite GetTargetImage()
        {
            return HumanIcons.GetHumanIcon(HumanType);
        }

        public void BeemHuman(Transform position, float speed)
        {
            transform.parent = position;
            transform.LerpTransform(this, position.position, speed);
        }

        public void RunAway(Transform point)
        {
            Agent.speed *= 2;
            newPosition = new Vector3(point.position.x, 0, point.position.z);
        }
    }
}
