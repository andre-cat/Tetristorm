using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{

    [Header("For the basic movement")]
    [Space(10)]
    [Range(0.0f, 10.0f)]
    [SerializeField] private float speed = 5.0f;

    [Header("For the Sine Movement")]
    [Space(5)]
    [SerializeField] private float amplitude = 2.0f;
    [SerializeField] private float frequency = 0.5f;
    private float sinCenterY;

    public bool isInverted = false;


    void Start()
    {
        sinCenterY = transform.position.y;
    }

    void FixedUpdate()
    {
        BasicMovement();
        SineWaveMovement();
        
    }

    void SineWaveMovement()
    {
        Vector3 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        if (isInverted)
        {
            sin *= -1;
        }
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }

    void BasicMovement()
    {
        Vector3 pos = transform.position;

        pos.x -= speed * Time.fixedDeltaTime;



        transform.position = pos;
    }
}
