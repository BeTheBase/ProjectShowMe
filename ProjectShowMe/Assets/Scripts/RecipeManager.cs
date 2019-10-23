using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;

[System.Serializable]
public struct RecipeCombo
{
    [SerializeField] private List<HumanType> humanTypes;
    public List<HumanType> HumanTypes { get => humanTypes; set => humanTypes = value; }
    [SerializeField] private int satisfaction;
    public int Satisfaction { get => satisfaction; set => satisfaction = value; }
}
public class RecipeManager : MonoBehaviour
{
    [SerializeField] private List<RecipeCombo> combos;

    private string jsonString;

    private void OnEnable()
    {
        EventManager<string>.AddHandler(EVENT.recipeManagerSaved, SaveManagerData);
        EventManager<string>.AddHandler(EVENT.recipeManagerLoad, LoadManagerData);
    }

    private void Start()
    {
        Recipes.Combos = combos;
        combos = JsonConverter<RecipeCombo>.FromJson(jsonString, "/RecipeManagerData.json");
    }

    public void SaveManagerData(string j)
    {
        jsonString = JsonConverter<RecipeCombo>.SerializeToJson(combos, jsonString, "/RecipeManagerData.json");
        Debug.Log(jsonString);
    }

    public void LoadManagerData(string j)
    {
        combos = JsonConverter<RecipeCombo>.FromJson(jsonString, "/RecipeManagerData.json");
    }
}
