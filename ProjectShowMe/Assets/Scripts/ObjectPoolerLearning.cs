using System.Collections.Generic;
using UnityEngine;
using System;
using Bas.Interfaces;
using UnityEngine.SceneManagement;

public class ObjectPoolerLearning : GenericSingleton<ObjectPoolerLearning, IMonoPool>, IMonoPool
{
    private Dictionary<Type, Queue<MonoBehaviour>> poolDictionary = new Dictionary<Type, Queue<MonoBehaviour>>();

    //private List<PoolableContainerClass>

    private const int STARTAMOUNT = 1;

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
        //Load All poolable objects found in the Resources/PoolableObjects
        UnityEngine.Object[] allPoolableObjects = Resources.LoadAll("PoolableObjects", typeof(MyScriptableObjectClass));
        foreach (MyScriptableObjectClass prototype in allPoolableObjects)
        {
            if(!poolDictionary.ContainsKey(prototype.Prefab.GetType()))
                poolDictionary.Add(prototype.Prefab.GetType(), new Queue<MonoBehaviour>());

            for (int i = 0; i < prototype.Size; i++)
            {
                MonoBehaviour poolableObj = Instantiate(prototype.Prefab);
                poolableObj.gameObject.SetActive(false);
                poolDictionary[poolableObj.GetType()].Enqueue(poolableObj);
            }
        }
    }

    public MonoBehaviour GetObjectFromPool<T>() where T : MonoBehaviour
    {
        if (poolDictionary.ContainsKey(typeof(T)))
        {
            T objectToSpawn = poolDictionary[typeof(T)].Dequeue() as T;

            poolDictionary[typeof(T)].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        return default;
    }

    public MonoBehaviour SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : MonoBehaviour
    {
        if (!poolDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogWarning("Pool with type: " + typeof(T) + " doesn't exist.");
            return null;
        }

        if (poolDictionary.Count > 1)
        {
            MonoBehaviour objectToSpawn = poolDictionary[typeof(T)].Dequeue();
            objectToSpawn.gameObject.SetActive(true);
            objectToSpawn.gameObject.transform.position = position;
            objectToSpawn.gameObject.transform.rotation = rotation;
            poolDictionary[typeof(T)].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        else
        {
            MonoBehaviour obj = poolDictionary[typeof(T)].Peek();
            obj = UnityEngine.Object.Instantiate(obj);
            return obj;
        }
    }

    public void Recycle<T>(IPoolable owner)
    {

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