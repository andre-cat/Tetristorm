using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool canRotate = true;

    GameObject[] squareFx;
    public string squareFxTag = "SquareFX";

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }
    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void RotateRight()
    {
        if (canRotate)
            transform.Rotate(0, 0, -90);
    }

    public void RotateLeft()
    {
        if (canRotate)
            transform.Rotate(0, 0, 90);
    }

    void Start()
    {
        if(squareFxTag != "")
        {
            squareFx = GameObject.FindGameObjectsWithTag(squareFxTag);
        }       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShapeLandedFX()
    {
        int index = 0;

        foreach(Transform child in gameObject.transform)
        {
            if (squareFx[index])
            {
                squareFx[index].transform.position = new Vector3(child.position.x, child.position.y+1, -1);
                ParticleSquare squareParticle = squareFx[index].GetComponent<ParticleSquare>();
                if (squareParticle)
                {
                    squareParticle.Play();
                }
            }
            index++;
        }
    }
}
