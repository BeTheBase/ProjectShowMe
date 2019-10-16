using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonoPool
{
    MonoBehaviour SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : MonoBehaviour;
}
