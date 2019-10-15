using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTrigger : MonoBehaviour
{
    private bool InRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HUMAN")
        {
            InRange = true;
            //EventManager<bool>.AddHandler(EVENT.humanDetectEvent,)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HUMAN")
        {
            InRange = false;
        }
    }
}
