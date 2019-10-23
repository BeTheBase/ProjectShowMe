using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScoreData
{
    [SerializeField] [HideInInspector] public int id;
    [SerializeField] public string name;
    [SerializeField] public int score;
    [SerializeField] public int humansCollected;
}
