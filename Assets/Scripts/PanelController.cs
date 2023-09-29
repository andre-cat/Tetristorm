using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    public float dropInterval = 0.2f;
    float timeToDrop;

    ScoreManager scoreManager;

    float keyCoolDown;
    public float keyRepeatRate = 0.1f; 

    bool gameOver = false;
    void Start()
    {
        keyCoolDown = Time.time;
        gameBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

        if (!gameBoard)
        {
            Debug.Log("Not board set");
        }
        if (spawner)
        {
            spawner.transform.position = Vector3Int.RoundToInt(spawner.transform.position);
            if (activeShape == null)
            {
                activeShape = spawner.SpawnShape();
            }
        }
        else
        {
            Debug.Log("Not spawner set");
        }

        if (!scoreManager)
        {
            Debug.Log("Create a Score Manager");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameBoard || !spawner || !activeShape || !scoreManager || gameOver)
        {
            return;
        }

        CheckInput();     
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.D) && Time.time > keyCoolDown)
        {
            activeShape.MoveRight();
            keyCoolDown = Time.time + keyRepeatRate;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveLeft();
            }
           
        }
        else if (Input.GetKey(KeyCode.A) && Time.time > keyCoolDown)
        {
            activeShape.MoveLeft();
            keyCoolDown = Time.time + keyRepeatRate;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveRight();
            }
            
        }
        else if (Input.GetKey(KeyCode.W) && Time.time > keyCoolDown)
        {
            activeShape.RotateRight();
            keyCoolDown = Time.time + keyRepeatRate;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.RotateLeft();
            }

        }
        else if(Input.GetKey(KeyCode.S) && Time.time > keyCoolDown || Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            keyCoolDown = Time.time + keyRepeatRate;
            
            activeShape.MoveDown();

            if (!gameBoard.IsValidPosition(activeShape))
            {
                if (gameBoard.IsInLimit(activeShape))
                {
                    GameOver();
                }
                else
                {
                    StopShapeLanded();
                }
            }
            
        }
    }

    void StopShapeLanded()
    {
        keyCoolDown = Time.time;
        activeShape.MoveUp();

        gameBoard.SetPositionShapeInGrid(activeShape);
        activeShape = spawner.SpawnShape();

        gameBoard.ClearAllRows();

        if(gameBoard.completedRows > 0)
        {
            //Add sound effect

            scoreManager.ScoreMultiplier(gameBoard.completedRows);
        }
    }

    void GameOver()
    {
        activeShape.MoveUp();
        Debug.Log("Limit reached");
        gameOver = true;
    }
}
