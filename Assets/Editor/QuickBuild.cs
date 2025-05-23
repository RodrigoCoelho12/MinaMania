using UnityEditor;
using UnityEngine;

public class QuickBuildTool
{
    [MenuItem("Tools/Arthur Roque/Utilities/Quick Build")]
    public static void QuickBuild()
    {
        string[] scenes = { "Assets/Scenes/MainScene.unity" };
        BuildPipeline.BuildPlayer(scenes, "Builds/QuickBuild.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        Debug.Log("Quick build completed!");
    }
}