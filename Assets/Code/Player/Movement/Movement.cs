using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] WheelCollider FrontLeft;
    [SerializeField] WheelCollider FrontRight;
    [SerializeField] WheelCollider BackLeft;
    [SerializeField] WheelCollider BackRight;

    public float acceleration = 500f;
    public float breaks = 300f;
    public float maxturnangle = 15f;

    private float currentAcceleration = 0f;
    private float currentbreak = 0f;
    private float currentturnangle = 0f;



    void FixedUpdate()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");
       

        if (Input.GetKey(KeyCode.Space))
        {
            currentbreak = breaks;
        }
        else
        {
            currentbreak = 0f;
        }

        FrontRight.motorTorque = currentAcceleration;
        FrontLeft.motorTorque = currentAcceleration;

        FrontRight.brakeTorque = currentbreak;
        FrontLeft.brakeTorque = currentbreak;

        currentturnangle = maxturnangle * Input.GetAxis("Horizontal");
        FrontLeft.steerAngle = currentturnangle;
        FrontRight.steerAngle = currentturnangle;

    }

    public void Unflip()
    {
        var r = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(r.x, r.y, 0);
    }
}
