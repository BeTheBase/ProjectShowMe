using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

public class HumanManager : MonoBehaviour
{
    [SerializeField] private List<HumanData> humans;
    [SerializeField] private List<Transform> spawnAreas;
    [SerializeField] private int maxHumans;
    [SerializeField] public string jsonString;

    private void OnEnable()
    {
        EventManager<string>.AddHandler(EVENT.managerLoadEvent, LoadManagerData);
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, ReSpawn);
    }

    private void Start()
    {
        //humans = JsonConverter<HumanData>.FromJson(jsonString, "/HumanManagerData.json");

        /*
         * TODO:
         * We hebben een maxaantal humans. Dit is de 100% 
         * We willen door alle humans lopen en deze percentueel inspawnen. 
         * 
         */
        HumanIcons.Init();

        InitHumans(maxHumans);
    }

    public void SaveManagerData(string j)
    {
        jsonString = JsonConverter<HumanData>.SerializeToJson(humans, jsonString, "/HumanManagerData.json");
        Debug.Log(jsonString);
    }

    public void LoadManagerData(string j)
    {
        humans = JsonConverter<HumanData>.FromJson(jsonString, "/HumanManagerData.json");
    }

    private void Spawn(GameObject prefab, Vector3 position, Quaternion rotation, HumanType humanType, List<Transform> patrolPoints)
    {
        //GameObject human = Instantiate(prefab, position, rotation);
        GameObject human = ObjectPoolerTypes.Instance.SpawnFromPool(prefab, position, rotation);
        ITargetable target = human.GetComponent<ITargetable>();
        target.SetHumanType(humanType);
        target.SetPatrolPoints(patrolPoints);      
    }

    private void InitHumans(int amount)
    {/*
        for (int humanAmount = 0; humanAmount < amount; humanAmount++)
        {
            foreach (HumanData humanData in humans)
            {
                if (Random.value > humanData.percentage)
                {
                    Spawn(humanData.prefab, spawnAreas[Random.Range(0, spawnAreas.Count)].position, Quaternion.identity, humanData.humanType, humanData.patrolPoints);
                    break;
                }
            }
        }*/

        foreach(HumanData humanData in humans)
        {
            for(int index = 0; index < (humanData.percentage/100 * amount); index++)
            {
                Spawn(humanData.prefab, spawnAreas[Random.Range(0, spawnAreas.Count)].position, Quaternion.identity, humanData.humanType, humanData.patrolPoints);
            }
        }
    }

    public void ReSpawn(int x)
    {
        InitHumans(3);
    }


}
