using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class MyScriptableObjectClass : ScriptableObject
{
    [SerializeField] private MonoBehaviour prefab;
    public MonoBehaviour Prefab { get => prefab; set => prefab = value; }

    [SerializeField] private int size;
    public int Size { get => size; set => size = value; }
}
