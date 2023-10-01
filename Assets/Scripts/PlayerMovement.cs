using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [Header("REFERENCES")]
    [SerializeField] private Board board;

    [Header("MOVEMENT")]

    [Header("-- Roll | left-right")]
    [SerializeField] private float rotationRoll = 0;
    [SerializeField] private float maxRotationRoll = 0;

    [Header("-- Pitch | up-down")]
    [SerializeField] private float rotationPitch = 0;
    [SerializeField] private float maxRotationPitch = 0;

    [Header("-- Return")]
    [SerializeField] private float returnSpeed = 0;

    [Header("TURBULENCE")]

    [SerializeField] private float turbulenceAmplitude = 0f;
    [SerializeField] private float turbulenceFrequency = 0f;

    private Rigidbody body;
    private Vector3 firstPosition;

    private bool isPitching;
    private bool isYawing;
    private bool isRolling;

    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        isYawing = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Roll();
        Pitch();
        //Yaw();

        Return();

        switch (Board.levelReached)
        {
            case 0:
                break;
            case 1:
                SufferTurbulence(0.25f * turbulenceAmplitude, 0.25f * turbulenceFrequency);
                break;
            case 2:
                SufferTurbulence(0.50f * turbulenceAmplitude, 0.50f * turbulenceFrequency);
                break;
            case 3:
                SufferTurbulence(0.75f * turbulenceAmplitude, 0.75f * turbulenceFrequency);
                break;
            case 4:
                SufferTurbulence(turbulenceAmplitude, turbulenceFrequency);
                break;
        }
    }

    private void Roll() // left-right
    {
        float roll = Input.GetAxis("Horizontal");
        isRolling = Mathf.Abs(roll) > 0;

        float rotation = GetMirrorAngle(transform.eulerAngles.z);

        if (rotation > -maxRotationRoll && rotation < maxRotationRoll)
        {
            transform.Rotate(roll * rotationRoll * Time.deltaTime * -Vector3.forward);
        }
    }

    private void Pitch() // up-down
    {
        float pitch = Input.GetAxis("Vertical");
        isPitching = Mathf.Abs(pitch) > 0;

        float rotation = GetMirrorAngle(transform.eulerAngles.x);

        if (rotation > -maxRotationPitch && rotation < maxRotationPitch)
        {
            transform.Rotate(pitch * rotationPitch * Time.deltaTime * Vector3.right);
        }
    }

    //private void Yaw(){}

    private void Return()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (!isPitching) { currentRotation.x = 0f; }
        if (!isYawing) { currentRotation.y = 0f; }
        if (!isRolling) { currentRotation.z = 0f; }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(currentRotation), Time.deltaTime * returnSpeed);
    }

    private void SufferTurbulence(float turbulenceAmplitude, float turbulenceFrequency)
    {
        float noiseRange = 2f - 1f;
        
        float offsetX = Time.time * turbulenceFrequency;
        float offsetY = Time.time * turbulenceFrequency;
        float offsetZ = Time.time * turbulenceFrequency;
        float turbulenceX = Mathf.PerlinNoise(offsetX, 0) * noiseRange;
        float turbulenceY = Mathf.PerlinNoise(0, offsetY) * noiseRange;
        float turbulenceZ = Mathf.PerlinNoise(offsetZ, 0) * noiseRange;

        Vector3 turbulence = new Vector3(turbulenceX, turbulenceY, turbulenceZ) * turbulenceAmplitude;
        transform.position = firstPosition + turbulence;
    }

    private float GetMirrorAngle(float degree)
    {
        if (degree > 180f)
        {
            degree -= 360f;
        }
        return degree;
    }
}
