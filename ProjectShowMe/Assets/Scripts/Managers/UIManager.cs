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
    [SerializeField] private float babyBarLerpSpeed = 2f;
    [SerializeField] private Image collectedHumanCount;
    [SerializeField] private int pointsForCompletedBar = 100;
    private bool updateBabyBar = false;
    private bool updateHumanCount = false;
    private float newValue;
    private float newFillAmount;

    private void OnEnable()
    {
        //Add the updatebabybar method to the gameupdate event
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, UpdateBabyBar);
        EventManager<ITargetable>.AddHandler(EVENT.humanDetectEvent, UpdateHumanCount);
        EventManager<int>.AddHandler(EVENT.barCompletedEvent, ClearBabyBar);
    }

    /// <summary>
    /// Add or remove received percentage to the babyBar
    /// </summary>
    /// <param name="percentage"></param>
    public void UpdateBabyBar(int percentage)
    {
        newValue = babyBar.value + percentage;
        updateBabyBar = true;
        ClearHumanCount();
    }

    public void UpdateHumanCount(ITargetable empty)
    {
        newFillAmount = collectedHumanCount.fillAmount + (100f / 3f)/100f;
        updateHumanCount = true;
    }

    public void ClearBabyBar(int empty)
    {
        newValue = 0;
        babyBar.value = 0;
        updateBabyBar = true;
    }

    public void ClearHumanCount()
    {
        newFillAmount = 0;
        collectedHumanCount.fillAmount = 0;
    }

    private void Update()
    {
        if(babyBar.value != babyBar.value + newValue && updateBabyBar)
        {
            babyBar.value = Mathf.Lerp(babyBar.value, newValue, babyBarLerpSpeed * Time.deltaTime);
        }
        if(collectedHumanCount.fillAmount != collectedHumanCount.fillAmount + newFillAmount && updateHumanCount)
        {
            collectedHumanCount.fillAmount = Mathf.Lerp(collectedHumanCount.fillAmount, newFillAmount, babyBarLerpSpeed * Time.deltaTime);
        }
        if(babyBar.value >= babyBar.maxValue)
        {
            EventManager<int>.BroadCast(EVENT.barCompletedEvent, pointsForCompletedBar);
        }
        if(babyBar.value <= 0)
        {
            Globals.OnGameOverEvent();
        }
    }
}
