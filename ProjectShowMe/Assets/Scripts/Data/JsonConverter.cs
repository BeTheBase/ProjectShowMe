﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class JsonConverter<T>
{

    /// <summary>
    /// Reads the Json file if found. And gives the levels list all the level data. 
    /// </summary>
    public static List<T> FromJson(string jsonString, string path)
    {
        if (File.Exists(Application.dataPath + path))
        {
            jsonString = File.ReadAllText(Application.dataPath + path);
        }
        if (jsonString == null || jsonString == string.Empty)
        {
            return null;
        }
        List<T> list = JsonHelper.FromJsonList<T>(jsonString);
        return list;
    }

    /// <summary>
    /// Serializes the levels list to a json string 
    /// </summary>
    /// <param name="x"></param>
    public static string SerializeToJson(List<T> list, string jsonString, string path)
    {
        if (list == null || list.Count < 1) return string.Empty;
        //Convert to Json
        jsonString = JsonHelper.ToJsonList(list);
        File.WriteAllText(Application.dataPath + path, jsonString);
        return jsonString;
    }
}