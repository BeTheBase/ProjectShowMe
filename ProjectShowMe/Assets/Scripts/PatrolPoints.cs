using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolSpots;
    public List<Transform> PatrolSpots { get => patrolSpots; set => patrolSpots = value; }
}
