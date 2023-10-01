using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Shape[] spawnShapes;

    Shape GetRandomShape()
    {
        int i = Random.Range(0, spawnShapes.Length);
        if (spawnShapes[i])
        {
            return spawnShapes[i];
        }
        else
        {
            Debug.Log("There's no shape");
            return null;
        }
    }

    public Shape SpawnShape()
    {
        Shape shape = null;
        shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;

        if (shape)
            return shape;
        else
            return null;
    }
    void Start()
    {
        Vector2 originalVector = new Vector2(4.3f, 1.3f);
        Vector2 newVector = Vector2Int.RoundToInt(originalVector);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
