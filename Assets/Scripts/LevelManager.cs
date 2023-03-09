using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator jungleAnimation;
    [SerializeField] private Animator level4Animation;
    [SerializeField] private PaperSocket[] paperSockets;
    [SerializeField] private GameObject jungle;
    [SerializeField] private Paper[] papersList;
   // [SerializeField] private GameObject[] closedPapers;
    public bool test = true;
    public int curr_level = 0;
    
    public static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

    }

    private void Start()
    {
        foreach (var socket in paperSockets)
        {
            socket.gameObject.SetActive(false);
        }
        for (int i = 1; i < papersList.Length; i++)
        {
            Debug.Log("HELLO");
            papersList[i].gameObject.SetActive(false);  
        }

    }

    private void Update()
    {

        //if (test) // JUST FOR TESTING
        //{
        //    LevelSetup();
        //}
    }

    /* BIG TO DO:
    OPENING SCREEN
    LIST CONTAINS ALL PAPER OBJECTS, WITH THE STORY ON THEM
    SOCKET FIX
     
     */
    public void LevelSetup()
    {
      //  paperSockets[curr_level - 1].gameObject.SetActive(true);

        if (curr_level == 1)
        {
            JungleArise();
            papersList[1].gameObject.SetActive(true);
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 2)
        {
            /* 
            TODO:
            Jungle Fade animation(sinking back maybe?)
            DARKNESS ALL BUT 2 LIGHTS 
            */
            JungleOut();
            jungle.SetActive(false);
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 3)
        {
            /* TODO: 
             * Light rise again
             * PLAYER SMALL (ANIMATION?)
             * paper appears under the table.
             
             */
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 4)
        {
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 5)
        {
            RunAndHideInABox();
        }
    }

    private void RunAndHideInABox()
    {
        level4Animation.SetTrigger("Start");
        StartCoroutine(WaitForAnimation(level4Animation));
    }

    private void JungleArise()
    {
        jungleAnimation.SetTrigger("jungle_arise");
        StartCoroutine(WaitForAnimation(jungleAnimation));

    }

    private void JungleOut()
    {
        jungleAnimation.SetTrigger("jungle_out");
        StartCoroutine(WaitForAnimation(jungleAnimation));

    }

    private IEnumerator WaitForAnimation(Animator animator)
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        GameManager.Instance.SetupReady = true;
    }
}