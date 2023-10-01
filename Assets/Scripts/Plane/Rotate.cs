using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float rotationZ;

    void Update()
    {
        Vector3 rotationVector = new Vector3(rotationX * Time.deltaTime, rotationY * Time.deltaTime, rotationZ * Time.deltaTime);
        transform.Rotate(rotationVector);
    }

}
