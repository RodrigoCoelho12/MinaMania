using UnityEngine;

public class HitBox : MonoBehaviour
{
    [Header("HitBox Properties")]
    [Tooltip("hitBoxColors: [0] =  close, [1] = medium, [2] = far")]
    public Color[] hitBoxColors = new Color[3];

    public void ChangeHitBoxColor(int index)
    {
        if (index < 0 || index >= hitBoxColors.Length)
        {
            Debug.LogError("Index out of range for hitBoxColors array.");
            return;
        }

        // Get the MeshRenderer component of the hitbox
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Change the color of the hitbox
            meshRenderer.material.color = hitBoxColors[index];
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on the hitbox.");
        }
    }

}