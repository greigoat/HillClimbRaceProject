using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    public WheelJoint2D[] wheels;
    public float mobility = 100.0F;
    public float acceleration = 10.0F;
    public float maxMotorTorque = 1000;
    public Rigidbody2D frameBody;
    public Vector2 startPos { get; private set; }
    public bool grounded { get; set; }

    public void AddMotorTorque(float motorTorque)
    {
        foreach (var w in wheels)
        {
            JointMotor2D m = w.motor;
            bool useMotor = w.useMotor;

            m.motorSpeed = -(motorTorque * (acceleration * 3600 * Time.fixedDeltaTime));

            w.motor = m;
            w.useMotor = useMotor;
        }
    }

    public void AddTorque(float torque)
    {
        if (!grounded)
        {
            // frameBody.angularVelocity += torque * mobility;
            frameBody.AddTorque(-torque * mobility,ForceMode2D.Impulse);
        }
    }


    public void Reset()
    {
        transform.root.rotation = Quaternion.identity;
        frameBody.velocity = Vector2.zero;
        frameBody.angularVelocity = 0;

        foreach (var wheel in wheels)
        {
            var useMotor = wheel.useMotor;
            JointMotor2D m = wheel.motor;
            m.motorSpeed = 0;
            wheel.motor = m;
            wheel.useMotor = useMotor;
        }
    }

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;

        if (!frameBody)
            frameBody = GetComponent<Rigidbody2D>();

        foreach (var wheel in wheels)
        {
            var useMotor = wheel.useMotor;
            JointMotor2D m = wheel.motor;
            m.maxMotorTorque = maxMotorTorque;
            wheel.motor = m;
            wheel.useMotor = useMotor;
        }
    }

    void FixedUpdate()
    {
    }
}
