using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlaneMovement : MonoBehaviour
{

    [Header("For the Sine Movement")]
    [Space(5)]
    [Range(0,3)]
    [SerializeField] private float amplitude = 2.0f;
    [Range(0.1f, 0.5f)]
    [SerializeField] private float frequency = 0.5f;
    private float sin;
    private float sinCenterY;

    void Start()
    {
        sinCenterY = transform.position.y;
        InvokeRepeating("ChangingValuesWithTime", 3.0f, 3.0f);
    }

    void FixedUpdate()
    {
        SineWaveMovement();
    }

    void SineWaveMovement() 
    {
        Vector3 pos = transform.position;

        sin = Mathf.Sin(pos.x * frequency) * amplitude;
        pos.y = sinCenterY + sin;
        transform.position = pos;
    }

    void ChangingValuesWithTime()
    {
        amplitude += 0.01f;
    }

}
