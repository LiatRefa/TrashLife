using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    #region BlockerMatrix
    [System.Serializable]
    public class Cell
    {
        public GameObject blocker;
    }

    [System.Serializable]
    public class Array
    {
        public List<Cell> cells = new List<Cell>();
        public Cell this[int index] => cells[index];

        public int Count
        {
            get { return cells.Count; }
        }
    }

    [System.Serializable]
    public class Matrix
    {
        public List<Array> arrays = new List<Array>();
        public Cell this[int x, int y] => arrays[x][y];

        public Array this[int index] => arrays[index];
    }
    
    #endregion

    private List<Action> gameStatesFunctions = new List<Action>();
    [SerializeField] private Transform leftCorner;
    [SerializeField] private Transform rightCorner;
    [SerializeField] private Matrix blockers;

    [SerializeField] private GameObject entranceBlocker;
    private Transform playerTransform;
    private Vector3 targetPosition;

    public float ThresholdForRunningAway = 2f;
    public float[] ThresholdForBlocking = new[] { 3f, 2f, 1f };
    public float moveSpeed = 2f;

    private bool isMoving;
    public bool firstEncounter = true;
    
    private void Start()
    {
        gameStatesFunctions.AddRange(new Action[]{Idle, RunAwayToTheCorner, BlockEntrance, RandomMove, HitDrawerAndBlock, FinalDialog });
        playerTransform = GameManager.Instance.player.transform;
        targetPosition = leftCorner.position;
        entranceBlocker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameManager.Instance.player.transform;
        gameStatesFunctions[GameManager.Instance.gameState]();
    }
    /// <summary>
    /// Do nothing, for game state = 0
    /// </summary>
    private void Idle()
    {
        
    }
    
    /// <summary>
    /// Every time the player gets too close run away to the other corner.
    /// </summary>
    private void RunAwayToTheCorner()
    {
        if (firstEncounter)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, leftCorner.position) < 0.1f)
            {
                firstEncounter = false;
            }
        }
        else
        {
            // If the player is within the distance threshold, set the target position to the opposite corner
            if (Vector3.Distance(transform.position, playerTransform.position) < ThresholdForRunningAway & !isMoving)
            {
                // If the target position is the left corner, set it to the right corner
                if (targetPosition == leftCorner.position)
                {
                    targetPosition = rightCorner.position;
                }
                // If the target position is the right corner, set it to the left corner
                else
                {
                    targetPosition = leftCorner.position;
                }

                isMoving = true;
            }

            if (isMoving)
            {
                // Move the trash can towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); 
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isMoving = false;
                }
                
            }
        }
    }
    
    /// <summary>
    /// Runs away from player and entrance becomes smaller the closer the player gets.
    /// </summary>
    private void BlockEntrance()
    {

        if (firstEncounter)
        {
            entranceBlocker.SetActive(true);
            firstEncounter = false;
        }
        for (int i = 0; i < ThresholdForBlocking.Length; i++)
        {
            float currentDis = ThresholdForBlocking[i];
            if (Vector3.Distance(transform.position, playerTransform.position) < currentDis)
            {
                Array currentBlocker = blockers[i];
                for (int j = 0; j <currentBlocker.Count; j++)
                {
                   currentBlocker[j].blocker.SetActive(true);
                }
            }
            else
            {
                Array currentBlocker = blockers[i];
                for (int j = 0; j <currentBlocker.Count; j++)
                {
                    currentBlocker[j].blocker.SetActive(false);
                }
            }
        }
    }
    
    /// <summary>
    /// Moves randomly in the room, entrance is blocked if too close.
    /// </summary>
    private void RandomMove()
    {
        
    }
    
    /// <summary>
    /// Hits the drawers, makes them open, runs to a corner and blocks itself with breakable wall.
    /// </summary>
    private void HitDrawerAndBlock()
    {
        
    }
    
    /// <summary>
    /// Needs to be constructed - what happands when the paper is thrown in.
    /// </summary>
    private void FinalDialog()
    {
        
    }
}
