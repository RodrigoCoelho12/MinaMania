using UnityEngine;
partial class Player
{
    private void MagnetEffect()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, currentMagnetData.magnetRange, currentMagnetData.magnetLayerMask);
        foreach (Collider item in items)
        {
            if (currentMagnetData.magnetAffectedItemTags.Contains(item.tag))
            {
                Vector3 direction = transform.position - item.transform.position;
                float distance = direction.magnitude;

                if (distance < currentMagnetData.magnetRange)
                {
                    Vector3 force = direction.normalized * currentMagnetData.magnetForce * Time.deltaTime;
                    item.transform.position += force;
                }
            }
        }
    }
}