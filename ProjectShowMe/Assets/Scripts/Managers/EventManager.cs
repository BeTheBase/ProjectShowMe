// <copyright file="EventManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EVENT { humanDetectEvent, checkPointEvent, managerDataSavedEvent, recipeManagerSaved, recipeManagerLoad, managerLoadEvent, gameUpdateEvent, reloadGame, initializeGame, saveGame, selectGame }; // ... Other events
/// <summary>
/// Generic event management/ this class is re-usable in every project
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EventManager<T>
{
    public delegate void GenericDelegate<A>(T c);

    // Stores the delegates that get called when an event is fired
    static Dictionary<EVENT, GenericDelegate<T>> genericEventTable = new Dictionary<EVENT, GenericDelegate<T>>();

    // Adds a delegate to get called for a specific event
    public static void AddHandler(EVENT evnt, GenericDelegate<T> action)
    {
        if (!genericEventTable.ContainsKey(evnt)) genericEventTable[evnt] = action;
        else genericEventTable[evnt] += action;
    }

    // Fires the event
    public static void BroadCast(EVENT evnt, T c)
    {
        if (genericEventTable[evnt] != null) genericEventTable[evnt](c);
    }

    //Un-subscribe the action
    public static void RemoveHandler(EVENT evnt, GenericDelegate<T> action)
    {
        if (!genericEventTable.ContainsKey(evnt)) genericEventTable[evnt] = action;
        else genericEventTable[evnt] -= action;
    }

    //Un-Subscribes the listeners to the event
    public static void RemoveHandlers(EVENT evnt)
    {
        if (!genericEventTable.ContainsKey(evnt))
        {
            return;
        }
        else
        {
            foreach (KeyValuePair<EVENT, GenericDelegate<T>> _delegate in genericEventTable)
            {
                genericEventTable[evnt] -= _delegate.Value;
            }
        }
    }

}




