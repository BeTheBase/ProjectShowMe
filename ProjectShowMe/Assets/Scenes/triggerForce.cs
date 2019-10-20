using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerForce : MonoBehaviour
{
    public float thrust = 1000f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            Rigidbody rBody = other.gameObject.GetComponent<Rigidbody>();
            if (rBody)
            {
                rBody.AddForce(Vector3.up * thrust);
                foreach(Collider col in other.gameObject.GetComponents<Collider>())
                {
                    col.enabled = false;
                }
            }
        }
    }
}
