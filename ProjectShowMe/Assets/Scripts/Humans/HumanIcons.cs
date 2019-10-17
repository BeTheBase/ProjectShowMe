using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Humans;
using System.Linq;
public static class HumanIcons
{

    static Object[] allHumanIcons = Resources.LoadAll("HumanIcons", typeof(ScriptableIcon));
    static List<ScriptableIcon> allIcons = new List<ScriptableIcon>();

    public static void Init()
    {
        foreach (ScriptableIcon sIcon in allHumanIcons)
        {
            allIcons.Add(sIcon);
        }
    }

    public static Image GetHumanIcon(HumanType humanType)
    {
        Image icon = allIcons.Find(ahi => ahi.HumanType.Equals(humanType)).HumanIcon;
        return icon == null ? null : icon;
    }
}
