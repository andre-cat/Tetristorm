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
    [Header("For the Sine Movement Horizontal")]
    [Space(5)]
    [SerializeField] private float amplitudeZ = 2.0f;
    [SerializeField] private float frequencyZ = 0.5f;

    private float sinCenterY;
    private float sinCenterZ;

    [Header("Booleans")]
    [Space(5)]
    public bool isInverted = false;
    public bool isInvertedZ = false;

    // Calls
    GameManager gameManager;

    void Start()
    {
        sinCenterY = transform.position.y;
        sinCenterZ = transform.position.z;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        BasicMovement();
        SineWaveMovement();
        SineWaveMovementHorizontal();
        // Prueba del crossfade
        
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

    void SineWaveMovementHorizontal()
    {
        //Halla el vector <pos> basado en la ubicación actual
        Vector3 pos = transform.position;

        // Hallamos la componente Sin, que es una relación del eje X (Coseno) y frecuencia que multiplica a la altitud para dar los nodos y valles
        float sin = Mathf.Sin(pos.x * frequencyZ) * amplitudeZ;

        /*if (isInverted)
        {
            sin *= -1; //Aquí solo invierte el sentido del seno para que el movimiento sea al contrario.
        }*/

        sin = isInvertedZ ? -sin : sin; // mi primera vez usando operadores Ternarios XD
        pos.z = sinCenterZ + sin;

        transform.position = pos;
    }

    void BasicMovement()
    {
        // Halla el vector pos de la posición actual del plane
        Vector3 pos = transform.position;
        // Altera nada más la posición en X (si queremos cambiar a otro ojo solo es cambiar la x) al ponerle sustraerle la velocidad 
        pos.x -= speed * Time.fixedDeltaTime;
        // Actualiza al nuevo valor de transform.position.
        transform.position = pos;

        // Es así de simple porque no habrá controlador por el humano del avión, solo es la cinemática de ser movido.
    }

}
