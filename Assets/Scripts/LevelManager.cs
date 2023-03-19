using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator jungleAnimation;
    [SerializeField] private Animator level4Animation;
    [SerializeField] private PaperSocket[] paperSockets;
    [SerializeField] private GameObject jungle;
    [SerializeField] private Paper[] papersList;
    [SerializeField] private Darkness darkness;
    [SerializeField] private Smaller smaller;
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
            papersList[i].gameObject.SetActive(false);  
        }

    }

    private void Update()
    {

    }

    /* BIG TO DO:
    OPENING SCREEN
    Boss Dialouge
   
     */
    public void LevelSetup()
    {
	GameManager2.Instance.trash.firstEncounter = true;
        paperSockets[curr_level - 1].gameObject.SetActive(true);

        if (curr_level == 1)
        {
            JungleArise();
            papersList[1].gameObject.SetActive(true);
           
        }
        else if (curr_level == 2)
        {
            JungleOut();
            jungle.SetActive(false);
            darkness.turnLights(false);
            papersList[2].gameObject.SetActive(true);
        GameManager2.Instance.SetupReady = true;
		
        }
        else if (curr_level == 3)
        {
            darkness.turnLights(true);
            // To implement: Zero Gravity
        GameManager2.Instance.SetupReady = true;
        }
        else if (curr_level == 4)
        {
            smaller.ChangeSize(true);
            papersList[3].gameObject.SetActive(true);
        GameManager2.Instance.SetupReady = true;

        }
        else if (curr_level == 5)
        {
            smaller.ChangeSize(false);
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
        AudioManager.Instance.PlayOneShotAttach(AudioManager.Sounds.TreeRising, gameObject);
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

        GameManager2.Instance.SetupReady = true;
    }


}