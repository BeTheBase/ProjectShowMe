using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

public class HumanManager : MonoBehaviour
{
    [SerializeField] private List<HumanData> humans;
    [SerializeField] private List<Transform> spawnAreas;

    private void Start()
    {
        for(int spawnIndex = 0; spawnIndex < spawnAreas.Count; spawnIndex++)
        {
            foreach(HumanData hData in humans)
            {
                for (int humanAmount = 0; humanAmount < hData.amount; humanAmount++)
                {
                    Spawn(spawnAreas[spawnIndex].position, Quaternion.identity, hData.humanType);
                }
            }
        }
    }

    private void Spawn(Vector3 position, Quaternion rotation, HumanType humanType)
    {
        GameObject human = ObjectPoolerLearning.Instance.SpawnFromPool<MonoBehaviour>(position, rotation).gameObject;
        human.GetComponent<ITargetable>().SetHumanType(humanType);
    }


}
