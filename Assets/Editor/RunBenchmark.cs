using UnityEditor;
using UnityEngine;

public class RunBenchmarkTool
{
    [MenuItem("Tools/Arthur Roque/Utilities/Run Benchmark")]
    public static void RunBenchmark()
    {
        float startTime = Time.realtimeSinceStartup;
        Debug.Log("Starting benchmark...");
        for (int i = 0; i < 1000000; i++) { }
        float endTime = Time.realtimeSinceStartup;
        float elapsedTime = endTime - startTime;
        Debug.Log($"Benchmark completed. Time taken: {elapsedTime} seconds.");
    }
}