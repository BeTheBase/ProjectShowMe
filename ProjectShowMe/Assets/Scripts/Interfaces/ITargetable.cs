﻿// <copyright file="ITargetable.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using Humans;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITargetable 
{
    Sprite GetTargetImage();
    HumanType GetHumanType();
    void BeemHuman(Transform position,float speed);
    void SetHumanType(HumanType humanType);
    void SetPatrolPoints(List<Transform> patrolPoints);
    void RunAway(Transform point);
    void Lock();
    void UnLock();
    void Remove();
}
