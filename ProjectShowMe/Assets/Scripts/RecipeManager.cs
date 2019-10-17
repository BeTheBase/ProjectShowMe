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

    private void Start()
    {
        Recipes.Combos = combos;
    }
}
