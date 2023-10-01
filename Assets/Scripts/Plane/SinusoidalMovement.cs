using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    [SerializeField] private float amplitudeX = 1f;
    [SerializeField] private float frequencyX = 1f;
    [SerializeField] private float velocityX = 1f;

    [SerializeField] private float amplitudeY = 1f;
    [SerializeField] private float frequencyY = 1f;
    [SerializeField] private float velocityY = 1f;

    [SerializeField] private float amplitudeZ = 1f;
    [SerializeField] private float frequencyZ = 1f;
    [SerializeField] private float velocityZ = 1f;

    private Vector3 iniPosition;

    void Start()
    {
        iniPosition = transform.position;
    }

    void Update()
    {
        float offsetX = amplitudeX * Mathf.Sin(Time.time * frequencyX * velocityX);
        float offsetY = amplitudeY * Mathf.Sin(Time.time * frequencyY * velocityY);
        float offsetZ = amplitudeZ * Mathf.Sin(Time.time * frequencyZ * velocityZ);

        transform.position = new Vector3(iniPosition.x + offsetX, iniPosition.y + offsetY, iniPosition.z + offsetZ);
    }
}
