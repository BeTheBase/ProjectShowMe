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

    //OnTriggerEnter
    /*We checken of we een human in de trigger zien
     * Als we een human zien, lock deze human, activeer timer en als de human x tijd in de trigger zit beemen we deze human
     * 
     *OnTirrgerExit
     * We unlocken de human die de trigger heeft verlaten en we resetten de timer. 
     */

    private IEnumerator OnTriggerEnter(Collider other)
    {
        //Check of er een human in de trigger is gekomen
        if(other.gameObject.tag == "HUMAN")
        {
            //Oke er is een human dus inrange wordt nu true
            InRange = true;
            ITargetable targetable = other.gameObject.GetComponent<ITargetable>();
            targetable?.Lock();
            yield return new WaitForSeconds(TimeToAdd);
            if(InRange)
            {
                EventManager<ITargetable>.BroadCast(EVENT.humanDetectEvent, targetable);
                Debug.Log("ADD");
                InRange = false;
            }
        }
        if (other.gameObject.tag == "CheckPoint")
        {
            CheckPointManager.Instance.ReachedCheckPoint();
        }
    }

     /*
    private void OnTriggerEnter(Collider other)
    {
        if (!InRange)
        {
            if (other.gameObject.tag == "HUMAN")
            {
                var time = TimeToAdd;
                InRange = true;
                ITargetable targetable = other.gameObject.GetComponent<ITargetable>();
                targetable?.Lock();
                StartCoroutine(Timer.Start(time, false, () =>
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
        }

    }*/

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "HUMAN")
        {
            InRange = false;
            ITargetable targetable = other.GetComponent<ITargetable>();
            targetable?.UnLock();
        }
    }
}
