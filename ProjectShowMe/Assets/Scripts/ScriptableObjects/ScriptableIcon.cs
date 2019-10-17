using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Humans;
public class ScriptableIcon : ScriptableObject
{
    [SerializeField] private HumanType humanType;
    public HumanType HumanType { get => humanType; set => humanType = value; }
    [SerializeField] private Image humanIcon;
    public Image HumanIcon { get => humanIcon; set => humanIcon = value; }
}
