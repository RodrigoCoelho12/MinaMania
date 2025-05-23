using UnityEditor;
using UnityEngine;

public class FolderStructureGeneratorTool : EditorWindow
{
    [MenuItem("Tools/Arthur Roque/Folder Structure Generator")]
    public static void ShowFolderStructureWindow()
    {
        GetWindow<FolderStructureGeneratorTool>("Folder Structure");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generate Folder Structure", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate"))
        {
            GenerateFolderStructure();
        }
    }

    private static void GenerateFolderStructure()
    {
        string root = "Assets";
        string[] folders = {
            "Documents/GDD", "Documents/GDR", "Documents/External Material", "Editor",
            "_Core/Scenes", "_Core/Scripts", "_Core/Settings", "_Core/Resources",
            "Art/Animations", "Art/Materials", "Art/Models", "Art/Textures",
            "Audio/Music", "Audio/SFX", "Audio/Dialogue",
            "Prefabs/Characters", "Prefabs/Environment", "Prefabs/UI",
            "SceneObjects/Level1", "SceneObjects/Level2", "SceneObjects/MainMenu",
            "UI/Fonts", "UI/Icons", "UI/Textures", "UI/Prefabs",
            "Shaders", "ThirdParty", "Testing/Playground", "Testing/Automation"
        };

        foreach (string folder in folders)
        {
            CreateFolderRecursively(root, folder);
        }

        AssetDatabase.Refresh();
        Debug.Log("Folder structure generated successfully!");
    }

    private static void CreateFolderRecursively(string root, string folderPath)
    {
        string[] parts = folderPath.Split('/');
        string currentPath = root;

        foreach (string part in parts)
        {
            string newFolderPath = $"{currentPath}/{part}";
            if (!AssetDatabase.IsValidFolder(newFolderPath))
            {
                AssetDatabase.CreateFolder(currentPath, part);
            }
            currentPath = newFolderPath;
        }
    }
}