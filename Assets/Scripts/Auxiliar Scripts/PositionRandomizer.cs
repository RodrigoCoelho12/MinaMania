using UnityEngine;
using System.Collections.Generic;

public class PositionRandomizer : MonoBehaviour
{
    [Header("[Position Randomizer] Properties")]
    public List<GameObject> positions = new List<GameObject>();
    public List<GameObject> prefab = new List<GameObject>();
    public GameObject parent;
    public int positionToFill;

    private List<bool> filledPositions = new List<bool>();

    int Randomize(int limit)
    {
        System.Random rand = new System.Random();
        return rand.Next(0, limit);
    }

    public void Positioning()
    {
        // Check if the lists are valid
        if (positions.Count == 0)
        {
            Debug.LogWarning("Position list is empty.");
            return;
        }

        if (prefab.Count == 0)
        {
            Debug.LogWarning("Prefab list is empty.");
            return;
        }

        if (positionToFill > positions.Count)
        {
            Debug.LogWarning("PositionToFill exceeds available positions.");
            positionToFill = positions.Count;
        }

        // Clear and reset filledPositions tracking
        filledPositions.Clear();
        filledPositions.AddRange(new bool[positions.Count]);

        int filledCount = 0;
        int maxAttempts = positions.Count * 3; // safety net to avoid infinite loop
        int attempts = 0;

        try
        {
            while (filledCount < positionToFill && attempts < maxAttempts)
            {
                int posIndex = Randomize(positions.Count);

                if (!filledPositions[posIndex])
                {
                    int prefabIndex = Randomize(prefab.Count);
                    GameObject randomPrefab = prefab[prefabIndex];

                    GameObject instance = Instantiate(randomPrefab, positions[posIndex].transform.position, Quaternion.identity);

                    if (parent != null)
                        instance.transform.SetParent(parent.transform);

                    filledPositions[posIndex] = true;
                    filledCount++;
                }

                attempts++;
            }

            if (attempts >= maxAttempts)
                Debug.LogWarning("Max attempts reached before filling all positions. Possible duplicates or constraints issue.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in Positioning: {e.Message}");
        }
    }
}
