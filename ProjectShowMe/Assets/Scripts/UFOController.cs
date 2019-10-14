using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    private List<ITargetable> targets = new List<ITargetable>();

    public GameObject UFO;
    public int ID = 0;
    public float UFOHeight = 6f;
    public float TimeToAdd = 20f;
    public float MaxTimeToAdd = 20f;
    private int maxTargetInventorySpace = 3;

    private void Update()
    {
        if (UFO == null) return;
        MoveUFO();
        Beem();
    }

    private void MoveUFO()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            UFO.transform.position = new Vector3(hit.point.x, UFOHeight, hit.point.z);
            
        }
    }

    private void Beem()
    {
        Vector3 dir = UFO.transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(UFO.transform.position, dir * 50, Color.blue);
        if (Physics.Raycast(UFO.transform.position, dir, out hit, Mathf.Infinity))
        {
            string name = hit.transform.name;
            if (name == "HUMAN")
            {
                ITargetable targetable = hit.transform.GetComponent<ITargetable>(); ;
                targetable?.Lock();
                AddWhenPosible(targetable);
            }
            else
            {
                TimeToAdd = MaxTimeToAdd;
            }
        }
    }

    private void AddWhenPosible(ITargetable targetable)
    {
        if (targets.Count <= maxTargetInventorySpace)
        {
            TimeToAdd -= Time.deltaTime;
            if (TimeToAdd <= 0)
            {
                Debug.Log(targets.Count);
                targets.Add(targetable);
                targetable.Remove();
                TimeToAdd = MaxTimeToAdd;
            }
        }
    }

    public void Target()
    {
        if (UFO == null) return;
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = Input.mousePosition;
            position.z = transform.position.z - Camera.main.transform.position.z;
            UFO.transform.position = Camera.main.ScreenToWorldPoint(position);
        }
        /*
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Input.GetTouch(i).position;
                UFO.transform.position = touchPosition;
                
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    ID = gameObject.GetHashCode();
                    int id = hit.GetHashCode();
                    string name = hit.transform.name;
                    if(id != ID)
                    {

                    }

                    if(name == "HUMAN")
                    {
                        ITargetable targetable = hit.transform.GetComponent<ITargetable>();
                        targetable.Lock();
                    }
                }

                }
            }*/
    }
}
