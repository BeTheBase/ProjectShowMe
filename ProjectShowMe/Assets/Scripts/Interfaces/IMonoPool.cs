using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonoPool
{
    GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation);
}
