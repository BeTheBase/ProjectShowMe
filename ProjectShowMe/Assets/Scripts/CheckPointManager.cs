// <copyright file="CheckPointManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

public class CheckPointManager : MonoBehaviour
{
    private void OnEnable()
    {
        //add the blend method to the checkpoint event
        EventManager<List<HumanType>>.AddHandler(EVENT.checkPointEvent, Blend);
    }

    /// <summary>
    /// Blend method where we calculate the satisfaction and broadcast the game update event with that satisfaction
    /// </summary>
    /// <param name="humanTypes">List of humantypes</param>
    public void Blend(List<HumanType> humanTypes)
    {
        EventManager<int>.BroadCast(EVENT.gameUpdateEvent,Recipes.RecipeChecker(humanTypes));
    }
}
