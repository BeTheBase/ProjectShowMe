using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;
public class MothershipManager : MonoBehaviour
{
    [SerializeField] private Transform lerpFromPoint;
    [SerializeField] private Transform lerpTowardsPoint;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private List<GameObject> mothershipComponents;
    private List<GameObject> Humans = new List<GameObject>();

    private void OnEnable()
    {
        EventManager<GameObject>.AddHandler(EVENT.humanCollected, AddHuman);
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, LerpHumansToMotherShip);
        EventManager<List<HumanType>>.AddHandler(EVENT.blendEvent, ActivateFeedBack);
    }

    private void Start()
    {
        foreach (GameObject component in mothershipComponents)
        {
            component.SetActive(false);
        }
    }

    public void AddHuman(GameObject human)
    {
        Humans.Add(human);
    }

    public void ActivateFeedBack(List<HumanType> empty)
    {
        foreach (GameObject component in mothershipComponents)
        {
            component.SetActive(true);
        }
    }

    public void LerpHumansToMotherShip(int r)
    {
        if (Humans.Count <1) return;
        foreach (GameObject human in Humans)
        {
            human.SetActive(true);
            human.transform.position = lerpFromPoint.position;
            //human.transform.LerpTransform(this, lerpTowardsPoint.position, lerpSpeed);
            StartCoroutine(Timer.Start(lerpSpeed, false, ()=>
            {
                human.SetActive(false);
                Humans.Remove(human);
                foreach (GameObject component in mothershipComponents)
                {
                    component.SetActive(false);
                }
            }));           
        }
    }
}
