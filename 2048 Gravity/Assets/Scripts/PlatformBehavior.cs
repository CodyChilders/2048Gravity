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
        Vector3 acc = Input.acceleration.normalized;
        if (Vector3.Magnitude(acc) != 0)
        {
            Debug.DrawRay(transform.position, acc, Color.cyan);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left, Color.cyan);
        }

        //angle on the x-axis
        float xAngle = Vector3.Angle(acc, Vector3.left);
        float zAngle = Vector3.Angle(acc, Vector3.forward);

        transform.rotation = initialOrientation;
        transform.Rotate(Vector3.left, xAngle);
        transform.Rotate(Vector3.forward, zAngle);
    }
}
