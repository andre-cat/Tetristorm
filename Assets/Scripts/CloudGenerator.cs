using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private Mesh cloudMesh;
    [SerializeField] private Material cloudMaterial;
    [SerializeField] private float cloudSize = 5;
    [SerializeField] private float maxScale = 1;
    [SerializeField] private float timeScale = 1;
    [SerializeField] private float texScale = 1;
    [SerializeField] private float minNoiseSize = 0.5f;
    [SerializeField] private float sizeScale = 0.25f;
    [SerializeField] private Camera _camera;
    [SerializeField] private int maxDistance;
    [SerializeField] private int batchesToCreate;
    [SerializeField] private Vector3 previewCameraPosition;
    [SerializeField] private float offsetX = 1;
    [SerializeField] private float offsetY = 1;
}
