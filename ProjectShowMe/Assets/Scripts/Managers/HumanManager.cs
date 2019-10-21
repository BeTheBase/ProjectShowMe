using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

public class HumanManager : MonoBehaviour
{
    [SerializeField] private List<HumanData> humans;
    [SerializeField] private List<Transform> spawnAreas;

    [SerializeField] public string jsonString;

    private void OnEnable()
    {
        EventManager<string>.AddHandler(EVENT.managerDataSavedEvent, SaveManagerData);
        EventManager<string>.AddHandler(EVENT.managerLoadEvent, LoadManagerData);
    }

    private void Start()
    {
        humans = JsonConverter<HumanData>.FromJson(jsonString, "/HumanManagerData.json");

        for (int spawnIndex = 0; spawnIndex < spawnAreas.Count; spawnIndex++)
        {
            foreach(HumanData hData in humans)
            {
                for (int humanAmount = 0; humanAmount < hData.amount; humanAmount++)
                {
                    Spawn(hData.prefab, spawnAreas[spawnIndex].position, Quaternion.identity, hData.humanType, hData.patrolPoints );
                }
            }
        }
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
        GameObject human = Instantiate(prefab, position, rotation);
        ITargetable target = human.GetComponent<ITargetable>();
        target.SetHumanType(humanType);
        target.SetPatrolPoints(patrolPoints);      
    }


}
