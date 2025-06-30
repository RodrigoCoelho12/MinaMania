using UnityEngine;
partial class Player
{
    public Collider[] items;
    private void MagnetEffect()
    {
        if (hasMagnet)
        {
            items = Physics.OverlapSphere(transform.position, currentMagnetData.magnetRange, currentMagnetData.magnetLayerMask);
            
            foreach (Collider item in items)
            {
                if (item.tag == "Mineral" || item.tag == "Drop")
                {
                    Debug.Log(item.name);
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
}