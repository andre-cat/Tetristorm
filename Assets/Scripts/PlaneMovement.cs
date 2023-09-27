using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{

    [Header("For the Sine Movement")]
    [Space(5)]
    [SerializeField] private float amplitude = 2.0f;
    [SerializeField] private float frequency = 0.5f;
    public float sin;

    private float sinCenterY;

    [Header("Booleans")]
    [Space(5)]
    public bool isInverted = false;

    // Calls
    public Animator animAirplane;

    //


    void Start()
    {
        sinCenterY = transform.position.y;
        animAirplane = GameObject.Find("Player").GetComponent<Animator>();
        InvokeRepeating("ChangingValuesWithTime", 3.0f, 3.0f);
    }

    void FixedUpdate()
    {
        SineWaveMovement();
    }

    void SineWaveMovement() 
    {
        //Halla el vector <pos> basado en la ubicación actual
        Vector3 pos = transform.position;

        // Hallamos la componente Sin, que es una relación del eje X (Coseno) y frecuencia que multiplica a la altitud para dar los nodos y valles
        float sin = Mathf.Sin(pos.x * frequency) * amplitude;

        /*if (isInverted)
        {
            sin *= -1; //Aquí solo invierte el sentido del seno para que el movimiento sea al contrario.
        }*/

        sin = isInverted ? -sin : sin; // mi primera vez usando operadores Ternarios XD
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }

    void ChangingValuesWithTime()
    {
        amplitude += 0.01f;
    }

}
