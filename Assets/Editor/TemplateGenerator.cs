using UnityEditor;
using UnityEngine;
using System.IO;

public class TemplateGeneratorTool
{
    [MenuItem("Tools/Arthur Roque/Scripts/Template Generator")]
    public static void TemplateGenerator()
    {
        string template = "using UnityEngine;\n\npublic class NewScript : MonoBehaviour\n{\n    void Start() { }\n    void Update() { }\n}";
        string path = "Assets/NewScriptTemplate.cs";
        File.WriteAllText(path, template);
        AssetDatabase.Refresh();
        Debug.Log($"Script template created at {path}");
    }
}