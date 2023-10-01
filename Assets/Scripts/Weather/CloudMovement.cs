using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    [SerializeField] private Transform ini;
    [SerializeField] private Transform end;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < end.transform.position.z)
        {
            transform.position = ini.position;
        }
    }
}
