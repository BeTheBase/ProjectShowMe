using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HumanManager))]
[CanEditMultipleObjects]
public class HumanManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Save Manager Data"))
        {
            EventManager<string>.BroadCast(EVENT.managerDataSavedEvent, "");
            Repaint();
        }

        if(GUILayout.Button("Load Manager Data"))
        {
            EventManager<string>.BroadCast(EVENT.managerLoadEvent, "");
            Repaint();
        }
    }
}
