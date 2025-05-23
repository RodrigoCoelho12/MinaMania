using UnityEditor;
using UnityEngine;

public class ReferenceCheckerTool
{
    [MenuItem("Tools/Arthur Roque/Assets/Reference Checker")]
    public static void ReferenceChecker()
    {
        string[] allAssets = AssetDatabase.FindAssets("t:Object");
        foreach (string asset in allAssets)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(asset);
            string[] dependencies = AssetDatabase.GetDependencies(assetPath);

            if (dependencies.Length == 1)
            {
                Debug.Log($"Asset {assetPath} is not referenced by any other assets.");
            }
        }
    }
}