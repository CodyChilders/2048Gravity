using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    Quaternion initialOrientation;

    void Start()
    {
        initialOrientation = transform.rotation;
    }

    void Update()
    {
        ChangeGravity();
    }

    void ChangeGravity()
    {
        Vector3 currentGravity = Physics.gravity;
        float length = currentGravity.magnitude;
        Vector3 accelerometer = Input.acceleration;
        if(accelerometer.magnitude == 0)
        {
            accelerometer = Vector3.back;
        }
        accelerometer = RotateForControls(accelerometer);
        Vector3 accelerometerNormalized = accelerometer.normalized;
        Physics.gravity = accelerometerNormalized * length;
    }

    Vector3 RotateForControls(Vector3 v)
    {
        Vector3 ret = Quaternion.Euler(-90, 0, 0) * v;
        ret.z *= -1;
        return ret;
    }
}
