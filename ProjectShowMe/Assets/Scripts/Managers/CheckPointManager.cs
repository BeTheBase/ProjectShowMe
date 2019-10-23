// <copyright file="CheckPointManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

public class CheckPointManager : GenericSingleton<CheckPointManager, ICheckPoint>, ICheckPoint
{
    private List<HumanType> humanTypes = new List<HumanType>();
    private bool blendReady = false;
    private void OnEnable()
    {
        //add the blend method to the checkpoint event
        EventManager<List<HumanType>>.AddHandler(EVENT.blendEvent, Blend);
    }

    /// <summary>
    /// Blend method where we calculate the satisfaction and broadcast the game update event with that satisfaction
    /// </summary>
    /// <param name="humanTypes">List of humantypes</param>
    public void Blend(List<HumanType> _humanTypes)
    {
        humanTypes = _humanTypes;
        blendReady = true;
    }

    public void ReachedCheckPoint()
    {
        if (blendReady)
        {
            EventManager<int>.BroadCast(EVENT.gameUpdateEvent, Recipes.RecipeChecker(humanTypes));
            Debug.Log("Feed the baby!");
            blendReady = false;
        }
        else
        {
            Debug.Log("Nah that Ain't a recipe yet");
            return;
        }
    }
}
