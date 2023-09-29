using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    public float dropInterval = 0.9f;
    float timeToDrop;

    ScoreManager scoreManager;


    //[Range(0.02f,1f)]
    //public float keyRepeatRate = 0.25f;

    [Range(0.02f, 1f)]
    public float keyRepeatRateHorizontal = 0.15f;

    [Range(0.01f, 1f)]
    public float keyRepeatRateDown = 0.01f;

    [Range(0.02f, 1f)]
    public float keyRepeatRateRotate = 0.25f;

    float keyCoolDownDown;
    float keyCoolDownHorizontal;
    float keyCoolDownRotate;

    bool gameOver = false;

    float horizontalInput;
    float verticalInput;
    void Start()
    {
        keyCoolDownDown = Time.time + keyRepeatRateDown;
        keyCoolDownHorizontal = Time.time + keyRepeatRateHorizontal;
        keyCoolDownRotate = Time.time + keyCoolDownRotate;
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

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");


        if (horizontalInput == 1 && Time.time > keyCoolDownHorizontal)
        {
            activeShape.MoveRight();
            keyCoolDownHorizontal = Time.time + keyRepeatRateHorizontal;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveLeft();
            }
           
        }
        else if (horizontalInput == -1 && Time.time > keyCoolDownHorizontal)
        {
            activeShape.MoveLeft();
            keyCoolDownHorizontal = Time.time + keyRepeatRateHorizontal;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveRight();
            }
            
        }
        else if (verticalInput == 1 && Time.time > keyCoolDownRotate)
        {
            activeShape.RotateRight();
            keyCoolDownRotate = Time.time + keyRepeatRateRotate;
            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.RotateLeft();
            }

        }
        else if(verticalInput == -1 && Time.time > keyCoolDownDown || Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            keyCoolDownDown = Time.time + keyRepeatRateDown;
            
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
        keyCoolDownDown = Time.time ;
        keyCoolDownHorizontal = Time.time ;
        keyCoolDownRotate = Time.time;
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
