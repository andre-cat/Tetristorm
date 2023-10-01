using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void LateUpdate()
    {
        Vector3 position = target.position;

        transform.position = position - offset;
    }
}
