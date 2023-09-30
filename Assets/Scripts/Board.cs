using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform squareSprite;
    public int height = 30;
    public int width = 10;

    public int header = 8;

    public int levelReached = 0;

    Transform[,] grid;
    public int completedRows = 0;

    SFXManager sfxManager;

    public ParticleSquare[] rowFX = new ParticleSquare[4];


    private void Awake()
    {
        grid = new Transform[width, height];
    }

    void Start()
    {
        transform.position = new Vector3(-20.34f, -10.45f, 1.82f);
        DrawEmptyCell();
        sfxManager = GameObject.Find("Canvas").GetComponent<SFXManager>();
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

    void Update()
    {
        CheckLevelBoard();
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
                    clone.gameObject.layer = LayerMask.NameToLayer("Board");
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


    public void CheckLevelBoard()
    {
       
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, (int)height / 2] == null && grid[x, (int)height / 8] == null && grid[x, (int)height / 4] == null && grid[x, height - header] == null)
            {
                levelReached = 0;
            }

                if (grid[x, (int)height / 8] != null && grid[x, (int)height / 4] == null)
            {
                levelReached = 1;
                return;
            }
            if (grid[x, (int)height / 4] != null && grid[x, (int)height / 8] != null && grid[x, (int)height / 2] == null)
            {
                levelReached = 2;
                return;
            }
            if (grid[x, (int)height / 2] != null && grid[x, (int)height / 8] != null && grid[x, (int)height / 4] != null && grid[x, height - (header+ 1)] == null)
            {
                levelReached = 3;
                return;
            }
            if (grid[x, height - (header + 1)] != null && grid[x, (int)height / 2] != null )
            {
                levelReached = 4;
                return;
            }            
        }
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if(grid[x,y] != null)
            {
                sfxManager.SFXTetris();
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

    public IEnumerator ClearAllRows()
    {
        completedRows = 0;

        for(int y=0; y < height; ++y)
        {
            if (IsRowComplete(y))
            {
                ClearRowFX(completedRows, y);
                completedRows++;
            }
        }
        yield return new WaitForSeconds(0.5f);

        for (int y=0; y < height; ++y)
        {
            if (IsRowComplete(y))
            {
                ClearRow(y);
                MoveRowsDown(y + 1);
                yield return new WaitForSeconds(0.3f);
                y--;
            }
        }
    }

    public bool IsInLimit(Shape shape)
    {
        foreach(Transform child in shape.transform)
        {
            if(child.transform.position.y >= (height - header - 1))
            {
                return true;
            }
        }
        return  false;
    }

    void ClearRowFX(int index, int y)
    {
        if (rowFX[index])
        {
            rowFX[index].transform.position = new Vector3(0, y, 14);
            rowFX[index].Play();
        }
    }
}
