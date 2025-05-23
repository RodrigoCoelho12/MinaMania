using UnityEditor;
using UnityEngine;

public class LightingTesterTool
{
    [MenuItem("Tools/Arthur Roque/Scenes/Lighting Tester")]
    public static void LightingTester()
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            light.intensity = Random.Range(0.5f, 1.5f);
            Debug.Log($"Tested light: {light.name}, Intensity: {light.intensity}");
        }
    }
}