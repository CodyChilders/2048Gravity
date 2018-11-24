using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    Quaternion initialOrientation;

    // Use this for initialization
    void Start()
    {
        initialOrientation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGravity();
        /*
        Vector3 acc = Input.acceleration.normalized;
        
        float xAngle = Vector3.Angle(acc, Vector3.down);
        //float zAngle = Vector3.Angle(acc, Vector3.forward);

        transform.rotation = initialOrientation;
        transform.Rotate(Vector3.forward, xAngle);
        //transform.Rotate(Vector3.forward, zAngle);*/
    }

    void ChangeGravity()
    {
        Vector3 currentGravity = Physics.gravity;
        float length = currentGravity.magnitude;
        Vector3 accelerometer = Input.acceleration;
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

    /*private void OnGUI()
    {
        Vector3 acc = Input.acceleration.normalized;
        string accStr = string.Format("[{0}, {1}, {2}]", acc.x, acc.y, acc.z);
        GUI.Label(new Rect(5, 5, 20, 20), accStr);
    }*/
}
