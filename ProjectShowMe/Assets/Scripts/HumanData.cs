using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

[System.Serializable]
public struct HumanData
{
    [SerializeField] public int amount;
    [SerializeField] public HumanType humanType;
    [SerializeField] public GameObject prefab;
    [SerializeField] public List<Transform> patrolPoints;
}
