using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItempRebound : MonoBehaviour
{
    private float degreesPerSecond = 15.0f;
    private float amplitude = 0.15f;
    private float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}