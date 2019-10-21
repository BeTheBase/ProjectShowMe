using UnityEngine;
using System;
using System.Collections.Generic;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static List<T> FromJsonList<T>(string json)
    {
        WrapperList<T> wrapperList = JsonUtility.FromJson<WrapperList<T>>(json);
        return wrapperList.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    public static string ToJsonList<T>(List<T> list)
    {
        WrapperList<T> wrapperList = new WrapperList<T>();
        wrapperList.Items = list;
        return JsonUtility.ToJson(wrapperList, true);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    [Serializable]
    private class WrapperList<T>
    {
        public List<T> Items;
    }
}