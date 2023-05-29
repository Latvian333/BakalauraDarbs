using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = -9.81f; // The force of gravity
    private Rigidbody rb; // The object's Rigidbody component

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply the force of gravity to the object
        Vector3 gravityForce = new Vector3(0.0f, gravity, 0.0f);
        rb.AddForce(gravityForce, ForceMode.Acceleration);
    }
}
