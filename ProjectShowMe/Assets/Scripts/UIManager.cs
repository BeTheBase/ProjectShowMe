// <copyright file="UIManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Bar representation of the happines of the baby
    /// </summary>
    [SerializeField] private Slider babyBar;

    private void OnEnable()
    {
        //Add the updatebabybar method to the gameupdate event
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, UpdateBabyBar);
    }

    /// <summary>
    /// Add or remove received percentage to the babyBar
    /// </summary>
    /// <param name="percentage"></param>
    public void UpdateBabyBar(int percentage)
    {
        babyBar.value += percentage;
    }
}
