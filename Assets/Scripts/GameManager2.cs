using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FMODUnity;
using Utils;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    private static GameManager2 instance;
    public GameObject player;
    [SerializeField] private XRDirectInteractor LeftHand;
    [SerializeField] private XRDirectInteractor RightHand;
    public StudioEventEmitter EventEmitter;
    public SerializableStack<Texture> paperTextures; // In case we decide to use a stack for the textures instead of the papers themselves
    public bool SetupReady { get; set; }

    public enum Scenes
    {
        MAIN,
        MainMenu,
        Office,
        Final
    }

    public static GameManager2 Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager2>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Start()
    {
        Invoke("StupidThing", 2);
        LoadScene(Scenes.MAIN);
        
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void StupidThing()
    {
        // ARTI'S CODE - TO CHECK IF NEEDED
        // You might be asking yourself WHYYYY??! and my answer is -> because it fixes a stupid bug i couldnt fix in any other awy.

        RightHand.enabled = false;
        RightHand.enabled = true;
        LeftHand.enabled = false;
        LeftHand.enabled = true;

    }

}
