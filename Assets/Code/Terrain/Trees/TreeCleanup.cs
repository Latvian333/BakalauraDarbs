using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TreeCleanup : MonoBehaviour
{
    void Start()
    {
        // Set the radius of the overlap sphere
        float radius = 0.5f;

        // Check if the object collides with any Water objects in the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Water"))
            {
                Destroy(gameObject);
            }
        }
    }
}
