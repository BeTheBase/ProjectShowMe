// <copyright file="InventoryManager.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Humans;
public class InventoryManager : GenericSingleton<InventoryManager, IInventory>, IInventory
{
    [SerializeField] private List<Image> humanInventoryImages;
    private int index = 0;
    [SerializeField] private List<HumanType> currentHumanTypes = new List<HumanType>();

    private void OnEnable()
    {
        EventManager<ITargetable>.AddHandler(EVENT.humanDetectEvent, SetItem);
    }

    public void SetItem(ITargetable targetable)
    {
        if (humanInventoryImages[index].IsActive()) index += 1;
        humanInventoryImages[index].gameObject.SetActive(true);
        currentHumanTypes.Add(targetable.GetHumanType());
        if(currentHumanTypes.Count>=3)
        {
            EventManager<List<HumanType>>.BroadCast(EVENT.checkPointEvent, currentHumanTypes);
        }
    }


}
