// <copyright file="UFOController.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> humanBeemClips;
    [SerializeField] private AudioSource audioSource;
    public float Speed = 5f;
    public GameObject UFO;
    public int ID = 0;
    public float UFOHeight = 6f;
    public float UFORayWidth = 5f;
    private List<ITargetable> targets = new List<ITargetable>();
    private float TimeToAdd = 2f;
    private float MaxTimeToAdd = 2f;
    private int maxTargetInventorySpace = 2;

    private void OnEnable()
    {
        EventManager<ITargetable>.AddHandler(EVENT.humanDetectEvent, AddTarget);
        EventManager<ITargetable>.AddHandler(EVENT.bombDetectEvent, AddTarget);
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, ClearTargets);
    }

    private void Update()
    {
        if (UFO == null) return;
        MoveUFO();
    }

    private void MoveUFO()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                Vector3 newPosition = new Vector3(hit.point.x, UFOHeight, hit.point.z);
                UFO.transform.position = Vector3.Lerp(UFO.transform.position, newPosition, Speed * Time.deltaTime);
            }
        }
    }

    /*
    private void Beem()
    {
        Vector3 dir = UFO.transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(UFO.transform.position, dir * 50, Color.blue);
        if (Physics.SphereCast(UFO.transform.position, UFORayWidth,dir, out hit, 20f))
        {
            string name = hit.transform.tag;
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
    }*/

    public void AddTarget(ITargetable targetable)
    {
        if (targetable == null) return;
        if(targets.Count <= maxTargetInventorySpace)
        {
            targets.Add(targetable);
            targetable.BeemHuman(UFO.transform, Speed);
            audioSource.clip = humanBeemClips[Random.Range(0, humanBeemClips.Count - 1)];
            audioSource.Play();
            StartCoroutine(Timer.Start(TimeToAdd, false, ()=>
            {
                targetable.Remove();
            }));
            Debug.Log(targetable);
        }
    }

    public void ClearTargets(int ready)
    {
         Debug.Log("Reset");
         targets.Clear();
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
                EventManager<ITargetable>.BroadCast(EVENT.humanDetectEvent, targetable);
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
