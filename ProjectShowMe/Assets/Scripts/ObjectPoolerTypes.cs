using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class ObjectPoolerTypes : GenericSingleton<ObjectPoolerTypes, IMonoPool>, IMonoPool
{
    private List<object> pools = new List<object>();
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    public override void Awake()
    {
        InitPools();
    }

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        base.OnLevelFinishedLoading(scene, mode);
        InitPools();
    }

    private void InitPools()
    {
        System.Object[] allPoolableObjects = Resources.LoadAll("PoolableObjects", typeof(MyScriptableObjectClass));
        foreach(MyScriptableObjectClass obj in allPoolableObjects)
        {
            if(!pools.Contains(obj))
                pools.Add(obj);
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (MyScriptableObjectClass pool in pools)
        {
            GameObject containerObject = new GameObject(pool.name);
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.Prefab, containerObject.transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj.gameObject);
            }
            if(!poolDictionary.ContainsKey(pool.Prefab.name))
                poolDictionary.Add(pool.Prefab.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab.name))
        {
            Debug.LogWarning("Pool with prefab: " + prefab + " doesn't exist.");
            return null;
        }
        Debug.Log(prefab.name);
        GameObject objectToSpawn = poolDictionary[prefab.name].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[prefab.name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public static UnityEngine.Object[] FindObjectsOfTypeByName(string aClassName)
    {
        var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            var types = assemblies[i].GetTypes();
            for (int n = 0; n < types.Length; n++)
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(types[n]) && aClassName == types[n].Name)
                    return UnityEngine.Object.FindObjectsOfType(types[n]);
            }
        }
        return new UnityEngine.Object[0];
    }
}
  
