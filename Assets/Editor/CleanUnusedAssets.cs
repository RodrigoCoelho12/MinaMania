using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CleanUnusedAssetsTool
{
    [MenuItem("Tools/Arthur Roque/Assets/Clean Unused Assets")]
    public static void CleanUnusedAssets()
    {
        string[] unusedAssets = AssetDatabase.FindAssets("t:Object");
        List<string> unusedAssetPaths = new List<string>();

        foreach (string asset in unusedAssets)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(asset);
            string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
            if (dependencies.Length == 1)
            {
                unusedAssetPaths.Add(assetPath);
            }
        }

        foreach (string assetPath in unusedAssetPaths)
        {
            AssetDatabase.DeleteAsset(assetPath);
            Debug.Log($"Unused asset deleted: {assetPath}");
        }

        AssetDatabase.Refresh();
    }
}