// <copyright file="UFOTrigger.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTrigger : MonoBehaviour
{
    public float TimeToAdd = 2f;
    private bool InRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HUMAN")
        {
            InRange = true;
            ITargetable targetable = other.gameObject.GetComponent<ITargetable>();
            targetable?.Lock();
            StartCoroutine(Timer.Start(TimeToAdd, false, () =>
            {
                // Do something at the end of the timer
                if (!InRange) return;
                Debug.Log("We can add now");
                EventManager<ITargetable>.BroadCast(EVENT.humanDetectEvent, targetable);
                InRange = false;
            }));

            //TO-DO
            //Als de trigger op meerdere humans tegelijk staat moet hij alleen de eerste human pakken 
            //De human moet dan omhoog lerpen naar de ufo en dan verdwijnen
        }

        if(other.gameObject.tag == "CheckPoint")
        {
            CheckPointManager.Instance.ReachedCheckPoint();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HUMAN")
        {
            StopAllCoroutines();
            InRange = false;
            ITargetable targetable = other.GetComponent<ITargetable>();
            targetable?.UnLock();
        }
    }
}
