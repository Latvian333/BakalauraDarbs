using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class TreeDistancePlayer : MonoBehaviour
{
    public float distanceThreshold = 60f; // Threshold for visibility in units
    public Transform player; // The player object

    void Update()
    {

        foreach (Transform child in transform)
        {
            float distance = Vector3.Distance(child.transform.position, player.position);

            if (distance <= distanceThreshold)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }

        }
    }


}
