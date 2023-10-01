using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    [SerializeField] private float speedRotation = 150.0f;
    void Update()
    {
        Vector3 rotationVector = Vector3.forward * speedRotation * Time.deltaTime;
        transform.Rotate(rotationVector);
    }
}
