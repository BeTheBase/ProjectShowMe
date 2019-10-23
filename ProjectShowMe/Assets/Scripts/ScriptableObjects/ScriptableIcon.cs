using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Humans;
public class ScriptableIcon : ScriptableObject
{
    [SerializeField] private HumanType humanType;
    public HumanType HumanType { get => humanType; set => humanType = value; }
    [SerializeField] private Sprite humanIcon;
    public Sprite HumanIcon { get => humanIcon; set => humanIcon = value; }
}
