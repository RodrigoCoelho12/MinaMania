using UnityEditor;
using UnityEngine;

public class QuickResolutionSwitcherTool
{
    [MenuItem("Tools/Arthur Roque/Utilities/Quick Resolution Switcher")]
    public static void QuickResolutionSwitcher()
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[Random.Range(0, resolutions.Length)];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Debug.Log($"Switched to resolution: {selectedResolution.width}x{selectedResolution.height}");
    }
}