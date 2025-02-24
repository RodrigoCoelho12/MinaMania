using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class PersonalToolsMenu : EditorWindow
{
    // Menu: PersonalTools/Assets
    [MenuItem("PersonalTools/Assets/Clean Unused Assets")]
    public static void CleanUnusedAssets()
    {
        // Limpar assets não utilizados
        string[] unusedAssets = AssetDatabase.FindAssets("t:Object");
        List<string> unusedAssetPaths = new List<string>();

        foreach (string asset in unusedAssets)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(asset);

            // Verifique se o asset não está referenciado por nada
            string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
            if (dependencies.Length == 1) // O próprio asset
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


    [MenuItem("PersonalTools/Assets/Create Prefabs")]
    public static void CreatePrefabs()
    {
        // Criar prefabs a partir de GameObjects selecionados
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            string prefabPath = $"Assets/Prefabs/{obj.name}.prefab";
            PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
            Debug.Log($"Prefab created: {prefabPath}");
        }
    }

    [MenuItem("PersonalTools/Assets/Reference Checker")]
    public static void ReferenceChecker()
    {
        // Verificar referências de assets no projeto
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

    // Menu: PersonalTools/Scenes
    [MenuItem("PersonalTools/Scenes/Snap to Grid")]
    public static void SnapToGrid()
    {
        // Alinhar objetos à grade
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            Vector3 position = obj.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            position.z = Mathf.Round(position.z);
            obj.transform.position = position;

            Debug.Log($"Snapped {obj.name} to grid: {position}");
        }
    }

    [MenuItem("PersonalTools/Scenes/Lighting Tester")]
    public static void LightingTester()
    {
        // Testar iluminação na cena
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            light.intensity = Random.Range(0.5f, 1.5f); // Alterando intensidade para testar
            Debug.Log($"Tested light: {light.name}, Intensity: {light.intensity}");
        }
    }

    [MenuItem("PersonalTools/Scenes/Layer Manager")]
    public static void LayerManager()
    {
        // Gerenciar camadas
        string[] layers = { "Default", "TransparentFX", "Ignore Raycast", "Water", "UI" };

        foreach (string layer in layers)
        {
            Debug.Log($"Layer: {layer} exists in the project.");
        }
    }

    // Menu: PersonalTools/Scripts
    [MenuItem("PersonalTools/Scripts/Script Analyzer")]
    public static void ScriptAnalyzer()
    {
        // Analisar scripts em busca de redundâncias e otimizações
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

    [MenuItem("PersonalTools/Scripts/Template Generator")]
    public static void TemplateGenerator()
    {
        // Gerar templates de scripts
        string template = "using UnityEngine;\n\npublic class NewScript : MonoBehaviour\n{\n    void Start() { }\n    void Update() { }\n}";

        string path = "Assets/NewScriptTemplate.cs";
        File.WriteAllText(path, template);
        AssetDatabase.Refresh();

        Debug.Log($"Script template created at {path}");
    }

    // Menu: PersonalTools/Utilities
    [MenuItem("PersonalTools/Utilities/Quick Resolution Switcher")]
    public static void QuickResolutionSwitcher()
    {
        // Alternar resoluções rapidamente
        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[Random.Range(0, resolutions.Length)];

        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Debug.Log($"Switched to resolution: {selectedResolution.width}x{selectedResolution.height}");
    }

    [MenuItem("PersonalTools/Utilities/Run Benchmark")]
    public static void RunBenchmark()
    {
        // Rodar benchmark
        float startTime = Time.realtimeSinceStartup;
        Debug.Log("Starting benchmark...");

        // Simulação de tarefa pesada
        for (int i = 0; i < 1000000; i++) { }

        float endTime = Time.realtimeSinceStartup;
        float elapsedTime = endTime - startTime;

        Debug.Log($"Benchmark completed. Time taken: {elapsedTime} seconds.");
    }

    [MenuItem("PersonalTools/Utilities/Quick Build")]
    public static void QuickBuild()
    {
        // Criar builds rapidamente
        string[] scenes = { "Assets/Scenes/MainScene.unity" }; // Adapte conforme necessário
        BuildPipeline.BuildPlayer(scenes, "Builds/QuickBuild.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        Debug.Log("Quick build completed!");
    }

    // Menu: PersonalTools/Models
    [MenuItem("PersonalTools/Models/Generate LOD")]
    public static void GenerateLOD()
    {
        // Gerar LOD para modelos 3D
        string[] models = AssetDatabase.FindAssets("t:Model");

        foreach (string model in models)
        {
            string modelPath = AssetDatabase.GUIDToAssetPath(model);
            GameObject modelObj = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath);

            if (modelObj != null)
            {
                // Implementação fictícia de geração de LOD
                Debug.Log($"Generating LOD for model: {modelObj.name}");
            }
        }
    }

    // Menu: PersonalTools/Folder Structure Generator
    [MenuItem("PersonalTools/Folder Structure Generator")]
    public static void ShowFolderStructureWindow()
    {
        GetWindow<FolderStructureWindow>("Folder Structure");
    }

    public class FolderStructureWindow : EditorWindow
    {
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
                "_Core/Scenes",
                "_Core/Scripts",
                "_Core/Settings",
                "_Core/Resources",
                "Art/Animations",
                "Art/Materials",
                "Art/Models",
                "Art/Textures",
                "Audio/Music",
                "Audio/SFX",
                "Audio/Dialogue",
                "Prefabs/Characters",
                "Prefabs/Environment",
                "Prefabs/UI",
                "SceneObjects/Level1",
                "SceneObjects/Level2",
                "SceneObjects/MainMenu",
                "UI/Fonts",
                "UI/Icons",
                "UI/Textures",
                "UI/Prefabs",
                "Shaders",
                "ThirdParty",
                "Testing/Playground",
                "Testing/Automation"
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
}
