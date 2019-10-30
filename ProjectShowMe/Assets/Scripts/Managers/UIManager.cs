// <copyright file="UIManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Bar representation of the happines of the baby
    /// </summary>
    [SerializeField] private Image babyBar;
    [SerializeField] private float babyBarLerpSpeed = 2f;
    //[SerializeField] private Image collectedHumanCount;
    [SerializeField] private int pointsForCompletedBar = 100;
    [SerializeField] private float decreaseAmount = 0.01f;
    [SerializeField] private float decreaseBoostTime = 60f;
    [SerializeField] private List<Image> lives;
    private bool updateBabyBar = false;
    private bool updateHumanCount = false;
    private float newValue;
    private float newFillAmount;
    private float gameTime = 0f;
    private void OnEnable()
    {
        //Add the updatebabybar method to the gameupdate event
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, UpdateBabyBar);
        //fillBar for collectedHumanCount
        //EventManager<ITargetable>.AddHandler(EVENT.humanDetectEvent, UpdateHumanCount);
        EventManager<int>.AddHandler(EVENT.barCompletedEvent, ClearBabyBar);
        Globals.OnLivesLostEvent += UpdatePlayerLives;
    }

    private void OnDisable()
    {
        Globals.OnLivesLostEvent -= UpdatePlayerLives;
    }

    private void Start()
    {
        StartCoroutine(DecreaseUp());
        StartCoroutine(Decrease());
    }

    public void UpdatePlayerLives()
    {
        if (lives.Count < 1) return;

        var find = lives.FindLast(i => i.gameObject.activeSelf);
        if(find == null)
        {
            Globals.OnGameOverEvent();
        }
        else
            find.gameObject.SetActive(false);
    }
    /// <summary>
    /// Add or remove received percentage to the babyBar
    /// </summary>
    /// <param name="percentage"></param>
    public void UpdateBabyBar(int percentage)
    {
        float newPercentage = percentage;
        newValue = babyBar.fillAmount + (newPercentage / 100f);
        updateBabyBar = true;
        //ClearHumanCount();
    }

    /*
    public void UpdateHumanCount(ITargetable empty)
    {
        newFillAmount = collectedHumanCount.fillAmount + (100f / 3f)/100f;
        updateHumanCount = true;
    }*/

    public void ClearBabyBar(int empty)
    {
        newValue = 1;
        babyBar.fillAmount = 1;
        updateBabyBar = true;
    }

    //fillBar for collectedHumanCount
/*
    public void ClearHumanCount()
    {
        newFillAmount = 0;
        collectedHumanCount.fillAmount = 0;
    }
    */
    private void Update()
    {
        if(babyBar.fillAmount != babyBar.fillAmount + newValue && updateBabyBar)
        {
            babyBar.fillAmount = Mathf.Lerp(babyBar.fillAmount, newValue, babyBarLerpSpeed * Time.deltaTime);
            StartCoroutine(SetBack());
        }
        if (babyBar.fillAmount >= 1)
        {
            EventManager<int>.BroadCast(EVENT.barCompletedEvent, pointsForCompletedBar);
        }
        if(babyBar.fillAmount <= 0)
        {
            Globals.OnGameOverEvent();
        }
    }
    private IEnumerator Decrease()
    {
        yield return new WaitForEndOfFrame();
        babyBar.fillAmount -= decreaseAmount;
        StartCoroutine(Decrease());
    }

    private IEnumerator SetBack()
    {
        yield return new WaitForSeconds(2f);
        updateBabyBar = false;
    }

    private IEnumerator DecreaseUp()
    {
        yield return new WaitForSeconds(decreaseBoostTime);
        decreaseAmount += 0.0001f;
        StartCoroutine(DecreaseUp());
    }
}
