using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTrigger : MonoBehaviour
{
    public float TimeToAdd = 2f;
    private bool InRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HUMAN")
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
                InventoryManager.Instance.SetItem(0);
            }));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HUMAN")
        {
            InRange = false;
            ITargetable targetable = other.GetComponent<ITargetable>();
            targetable?.UnLock();
        }
    }
}
