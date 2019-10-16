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
                    Spawn(hData.prefab, spawnAreas[spawnIndex].position, Quaternion.identity, hData.humanType, hData.patrolPoints );
                }
            }
        }
    }

    private void Spawn(GameObject prefab, Vector3 position, Quaternion rotation, HumanType humanType, List<Transform> patrolPoints)
    {
        GameObject human = Instantiate(prefab, position, rotation);
        ITargetable target = human.GetComponent<ITargetable>();
        target.SetHumanType(humanType);
        target.SetPatrolPoints(patrolPoints);      
    }


}
