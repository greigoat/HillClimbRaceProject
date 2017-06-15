using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    CarController car;

    void Start()
    {
        car = transform.root.GetComponent<CarController>();
    }

    void OnCollisionEnter2D()
    {
        car.grounded = true;
    }

    void OnCollisionExit2D()
    {
        car.grounded = false;
    }
}
