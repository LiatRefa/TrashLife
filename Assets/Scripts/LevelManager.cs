using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator level4Animation;
    [SerializeField] private PaperSocket[] paperSockets;
    [SerializeField] private GameObject jungle;
    public bool test;
    
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
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
        if (test) // JUST FOR TESTING
        {
            LevelSetup(5);
        }
    }

    public void LevelSetup(int level)
    {
        paperSockets[level - 1].gameObject.SetActive(true);

        if (level == 1)
        {
            jungle.SetActive(true); // Should change this to animation, make a new paper appear with this one.
            GameManager.Instance.SetupReady = true;
        }
        else if (level == 2)
        {
            GameManager.Instance.SetupReady = true;
        }
        else if (level == 3)
        {
            GameManager.Instance.SetupReady = true;
        }
        else if (level == 4)
        {
            GameManager.Instance.SetupReady = true;
        }
        else if (level == 5)
        {
            RunAndHideInABox();
        }
    }

    private void RunAndHideInABox()
    {
        level4Animation.SetTrigger("Start");
        StartCoroutine(WaitForAnimation(level4Animation));
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