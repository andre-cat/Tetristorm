using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbulentMovement : MonoBehaviour
{
    public float amplitudeX = 10f;
    public float amplitudeY = 10f;
    public float amplitudeZ = 10f;

    public float frequencyX = 1f;
    public float frequencyY = 1f;
    public float frequencyZ = 1f;

    void Update()
    {
        float x = amplitudeX * -Mathf.Sin(Time.time * frequencyX);
        float y = amplitudeY * -Mathf.Cos(Time.time * frequencyX);
        float z = amplitudeZ * -Mathf.Sin(Time.time * frequencyX);

        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
