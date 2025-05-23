using UnityEditor;
using UnityEngine;

public class ScriptAnalyzerTool
{
    [MenuItem("Tools/Arthur Roque/Scripts/Script Analyzer")]
    public static void ScriptAnalyzer()
    {
        string[] scripts = AssetDatabase.FindAssets("t:Script");

        foreach (string script in scripts)
        {
            string path = AssetDatabase.GUIDToAssetPath(script);
            TextAsset text = AssetDatabase.LoadAssetAtPath<TextAsset>(path);

            if (text != null)
            {
                string scriptContents = text.text;
                if (scriptContents.Contains("Debug.Log"))
                {
                    Debug.Log($"The script at {path} has Debug.Log calls that might be redundant.");
                }
            }
        }
    }
}