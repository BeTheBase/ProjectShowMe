// <copyright file="PatrolPoints.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolSpots;
    public List<Transform> PatrolSpots { get => patrolSpots; set => patrolSpots = value; }
}
