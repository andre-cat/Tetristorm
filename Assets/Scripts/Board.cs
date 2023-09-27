using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform squareSprite;
    public int height = 30;
    public int width = 10;

    public int header = 8;

    Transform[,] grid;


    private void Awake()
    {
        grid = new Transform[width, height];
    }

    void Start()
    {
        DrawEmptyCell();
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }

    bool IsOccupied(int x, int y, Shape shape)
    {
        return (grid[x, y] != null && grid[x, y].parent != shape.transform);
    }

    public bool IsValidPosition(Shape shape)
    {

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vector2Int.RoundToInt(child.position);
            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (IsOccupied((int)pos.x, (int)pos.y, shape))
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawEmptyCell()
    {
        if (squareSprite != null)
        {
            for (int y = 0; y < height - header; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone;
                    clone = Instantiate(squareSprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                    clone.name = "Board Space ( x = " + x.ToString() + ", y =" + y.ToString() + ")";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("Assing the squareSprite");
        }
    }

    public void SetPositionShapeInGrid(Shape shape)
    {
        if(shape == null)
        {
            return;
        }

        foreach(Transform child in shape.transform)
        {
            
            Vector2 pos = Vector2Int.RoundToInt(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool IsRowComplete(int y)
    {
        for (int x = 0; x<width; ++x)
        {
            if(grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if(grid[x,y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
            grid[x, y] = null;
        }
    }

    void MoveOneRowDown(int y)
    {
        for (int x=0; x< width; ++x)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x , y - 1].position += new Vector3(0, -1, 0);
            }

        }
    }

    void MoveRowsDown(int startY)
    {
        for(int i = startY; i < height; ++i)
        {
            MoveOneRowDown(i);
        }
    }

    public void ClearAllRows()
    {
        for(int y=0; y < height; ++y)
        {
            if (IsRowComplete(y))
            {
                ClearRow(y);
                MoveRowsDown(y + 1);
                y--;
            }
        }
    }
}
