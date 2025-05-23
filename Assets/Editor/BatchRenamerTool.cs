using UnityEditor;
using UnityEngine;

public class BatchRenamer : EditorWindow
{
    private string baseName = "NewName";
    private string prefix = "";
    private string suffix = "";
    private string toRemove = "";
    private string toReplace = "";
    private string replacement = "";
    private bool applyToOriginalName = false;

    [MenuItem("Tools/Arthur Roque/Utilities/Batch Renamer")]
    public static void ShowWindow()
    {
        GetWindow<BatchRenamer>("Batch Renamer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Rename Selected Objects", EditorStyles.boldLabel);
        
        applyToOriginalName = EditorGUILayout.Toggle("Apply To Original Name", applyToOriginalName);
        baseName = EditorGUILayout.TextField("Base Name", baseName);
        prefix = EditorGUILayout.TextField("Prefix", prefix);
        suffix = EditorGUILayout.TextField("Suffix", suffix);
        toRemove = EditorGUILayout.TextField("Text to Remove", toRemove);
        toReplace = EditorGUILayout.TextField("Text to Replace", toReplace);
        replacement = EditorGUILayout.TextField("Replacement", replacement);

        if (GUILayout.Button("Rename"))
        {
            RenameSelectedObjects();
        }
    }

    private void RenameSelectedObjects()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            Undo.RecordObject(obj, "Batch Rename");

            string nameToUse = applyToOriginalName ? obj.name : baseName;

            // Remove substring
            if (!string.IsNullOrEmpty(toRemove))
                nameToUse = nameToUse.Replace(toRemove, "");

            // Replace substring
            if (!string.IsNullOrEmpty(toReplace))
                nameToUse = nameToUse.Replace(toReplace, replacement);

            // Add prefix and suffix
            nameToUse = prefix + nameToUse + suffix;

            obj.name = nameToUse;
        }

        Debug.Log($"Renamed {selectedObjects.Length} object(s).");
    }
}
