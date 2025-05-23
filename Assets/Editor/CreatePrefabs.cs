using UnityEditor;
using UnityEngine;

public class CreatePrefabsTool
{
    [MenuItem("Tools/Arthur Roque/Assets/Create Prefabs")]
    public static void CreatePrefabs()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            string prefabPath = $"Assets/Prefabs/{obj.name}.prefab";
            PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
            Debug.Log($"Prefab created: {prefabPath}");
        }
    }
}