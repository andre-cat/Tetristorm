using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud
{

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;
    private bool isActive;
    private int x;
    private int y;
    private float distanceFromCamera;

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    public Quaternion Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    public Vector3 Scale
    {
        get => scale;
        set => scale = value;
    }

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public int X
    {
        get => x;
        set => x = value;
    }

    public int Y
    {
        get => y;
        set => y = value;
    }

    public float DistanceFromCamera
    {
        get => distanceFromCamera;
        set => distanceFromCamera = value;
    }

    public Matrix4x4 Matrix
    {
        get
        {
            return Matrix4x4.TRS(position, rotation, scale);
        }
    }

    public Cloud(Vector3 position, Quaternion rotation, Vector3 scale, int x, int y, float distanceFromCamera)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        isActive = true;
        this.x = x;
        this.y = x;
        this.distanceFromCamera = distanceFromCamera;
    }

}
