using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humans;
using System.Linq;

public static class Recipes 
{
    public static List<RecipeCombo> Combos;
    public static int satisfactionLevel0 = 5;
    public static int satisfactionLevel1 = 10;
    public static int satisfactionLevel2 = 15;
    public static int satisfactionLevel3 = 20;
    public static int satisfactionLevel4 = 25;
    public static int satisfactionLevel5 = 30;
    public static int satisfactionLevel6 = 35;
    public static int satisfactionLevel7 = 40;
    public static int satisfactionLevel8 = 45;
    public static int satisfactionLevel9 = 50;
    public static int satisfactionLevel10 = 55;
    public static int satisfactionLevel11 = 60;
    public static int satisfactionLevel12 = 65;
    public static int satisfactionLevel13 = 70;
    public static int satisfactionLevel14 = 80;

    public static int RecipeChecker(List<HumanType> humanTypes)
    {
        int currentComboSatisfaction = Combos.Find(c => c.HumanTypes.Equals(humanTypes)).Satisfaction;
        if (currentComboSatisfaction == 0)
            return -5;
        else
            return currentComboSatisfaction;
        /*
        HumanType humanType0 = humanTypes[0];
        HumanType humanType1 = humanTypes[1];
        HumanType humanType2 = humanTypes[2];

        switch(CheckSame(humanTypes))
        {
            case HumanType.Normal:
                return satisfactionLevel0;
            case HumanType.Fat:
                return -satisfactionLevel0;
            case HumanType.Strong:
                return satisfactionLevel1;
            case HumanType.Skinny:
                return satisfactionLevel1;
            case HumanType.Small:
                return satisfactionLevel0;
            case HumanType.Cool:
                return satisfactionLevel3;
            case HumanType.Rare:
                return satisfactionLevel7;
            case HumanType.Legendary:
                return satisfactionLevel14;
            default:
                return satisfactionLevel0;
        }*/
    }

    private static HumanType CheckSame(List<HumanType> humanTypes)
    {
        if(humanTypes.All(h => h.Equals(HumanType.Fat)))
        {
            return HumanType.Fat;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Normal)))
        {
            return HumanType.Normal;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Strong)))
        {
            return HumanType.Strong;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Skinny)))
        {
            return HumanType.Skinny;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Small)))
        {
            return HumanType.Small;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Cool)))
        {
            return HumanType.Cool;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Rare)))
        {
            return HumanType.Rare;
        }
        else if(humanTypes.All(h => h.Equals(HumanType.Legendary)))
        {
            return HumanType.Legendary;
        }
        else
        {
            Debug.Log("We don't have those types so we return the default type");
            return HumanType.Normal;
        }
    }
}
