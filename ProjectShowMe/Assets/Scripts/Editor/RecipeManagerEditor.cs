using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RecipeManager))]
[CanEditMultipleObjects]
public class RecipeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save Manager Data"))
        {
            EventManager<string>.BroadCast(EVENT.recipeManagerSaved, "");
            Repaint();
        }

        if (GUILayout.Button("Load Manager Data"))
        {
            EventManager<string>.BroadCast(EVENT.recipeManagerLoad, "");
            Repaint();
        }
    }
}