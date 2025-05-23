using UnityEditor;
using UnityEngine;

public class LayerManagerTool
{
    [MenuItem("Tools/Arthur Roque/Scenes/Layer Manager")]
    public static void LayerManager()
    {
        string[] layers = { "Default", "TransparentFX", "Ignore Raycast", "Water", "UI" };

        foreach (string layer in layers)
        {
            Debug.Log($"Layer: {layer} exists in the project.");
        }
    }
}