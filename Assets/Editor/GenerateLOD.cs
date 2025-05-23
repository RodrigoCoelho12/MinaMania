using UnityEditor;
using UnityEngine;

public class GenerateLODTool
{
    [MenuItem("Tools/Arthur Roque/Models/Generate LOD")]
    public static void GenerateLOD()
    {
        string[] models = AssetDatabase.FindAssets("t:Model");

        foreach (string model in models)
        {
            string modelPath = AssetDatabase.GUIDToAssetPath(model);
            GameObject modelObj = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath);

            if (modelObj != null)
            {
                Debug.Log($"Generating LOD for model: {modelObj.name}");
            }
        }
    }
}