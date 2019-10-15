using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Humans
{
    public enum HumanType
    {
        Normal = 350,
        Fat = 300,
        Strong = 150,
        Skinny = 80,
        Small = 60,
        Cool = 40,
        Rare = 20,
        Legendary = 1
    }
    
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Human : MonoBehaviour, ITargetable
    {
        [SerializeField] private float speed = 5f;

        private PatrolPoints PatrolPositions { get; set; }

        private Rigidbody RBody { get; set; }

        private NavMeshAgent Agent { get; set; }

        private int randomSpot = 0;

        public HumanType HumanType = HumanType.Normal;

        private void Start()
        {
            if (!RBody)
                RBody = GetComponent<Rigidbody>();
            if (!PatrolPositions)
                PatrolPositions = GetComponent<PatrolPoints>();
            if (!Agent)
                Agent = GetComponent<NavMeshAgent>();

            randomSpot = Random.Range(0, PatrolPositions.PatrolSpots.Count);
        }

        private void Update()
        {
            var newPointDistance = PatrolPositions.PatrolSpots[randomSpot].position - transform.position;
            var step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, newPointDistance, step, 0.0f);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(PatrolPositions.PatrolSpots[randomSpot].position.x, 0, PatrolPositions.PatrolSpots[randomSpot].position.z)) > 1f)
            {
                if(Agent.enabled)
                    Agent.SetDestination(PatrolPositions.PatrolSpots[randomSpot].position);
            }
            else
            {
                randomSpot = Random.Range(0, PatrolPositions.PatrolSpots.Count);
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
            gameObject.SetActive(false);
        }

        public HumanType GetHumanType()
        {
            return HumanType;
        }
    }
}
