using UnityEditor;
using UnityEngine;

public class SnapToGridTool
{
    [MenuItem("Tools/Arthur Roque/Scenes/Snap to Grid")]
    public static void SnapToGrid()
    {
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
}