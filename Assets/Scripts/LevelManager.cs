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

    }

    private void Update()
    {

        //if (test) // JUST FOR TESTING
        //{
        //    LevelSetup();
        //}
    }

    public void LevelSetup()
    {
      //  paperSockets[curr_level - 1].gameObject.SetActive(true);

        if (curr_level == 1)
        {
            //  jungle.SetActive(true); // Should change this to animation, make a new paper appear with this one.
            JungleArise();
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 2)
        {
            GameManager.Instance.SetupReady = true;
        }
        else if (curr_level == 3)
        {
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

    private IEnumerator WaitForAnimation(Animator animator)
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        GameManager.Instance.SetupReady = true;
    }
}