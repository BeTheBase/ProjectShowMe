using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipManager : MonoBehaviour
{
    [SerializeField] private Transform lerpFromPoint;
    [SerializeField] private Transform lerpTowardsPoint;
    [SerializeField] private float lerpSpeed = 5f;

    private List<GameObject> Humans = new List<GameObject>();

    private void OnEnable()
    {
        EventManager<GameObject>.AddHandler(EVENT.humanCollected, AddHuman);
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, LerpHumansToMotherShip);
    }

    public void AddHuman(GameObject human)
    {
        Humans.Add(human);
    }

    public void LerpHumansToMotherShip(int r)
    {
        if (Humans.Count <1) return;
        foreach (GameObject human in Humans)
        {
            human.SetActive(true);
            human.transform.position = lerpFromPoint.position;
            human.transform.LerpTransform(this, lerpTowardsPoint.position, lerpSpeed);
            human.transform.rotation = Quaternion.Euler(Random.Range(0, r), Random.Range(0, r), Random.Range(0, r));
            StartCoroutine(Timer.Start(lerpSpeed, false, ()=>
            {
                human.SetActive(false);
                Humans.Remove(human);
            }));           
        }
    }
}
