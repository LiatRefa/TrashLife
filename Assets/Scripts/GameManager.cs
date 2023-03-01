using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using Object = UnityEngine.Object;
using Utils;

public class GameManager : Singleton<GameManager>
{
    private bool firstUpdate;
    [SerializeField] private Animator _startAnimator;
    private float gameTimer;
    private float maxTime = 60 * 10f; // 10 mins
    public GameObject player;
    [SerializeField] private XRDirectInteractor LeftHand;
    [SerializeField] private XRDirectInteractor RightHand;
    [SerializeField] private LevelManager levelManager;
    public StudioEventEmitter EventEmitter;
    public SerializableStack<Texture> paperTextures; // In case we decide to use a stack for the textures instead of the papers themselves
    public Trash trash;

    public int gameState = 0;
    public bool SetupReady { get; set; }

    public enum Scenes
    {
        MAIN,
        MainMenu,
        EscapeRoom,
        Final
    }
    
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnMainSceneLoaded;
    }
    void Start()
    {
        // AudioManager.Instance.PlaySound(AudioManager.Sounds.ClockTicking);

    }
    void OnMainSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("StupidThing", 2);
        if (scene.name == "MAIN")
        {
            LoadScene(Scenes.MainMenu);
        }
        // if (scene.name == "MainOffice")
        // {
        //     StartCoroutine(WaitForLevelManager());
        // }
        
    }
    
    private IEnumerator WaitForLevelManager()
    {
        while (LevelManager.instance == null)
        {
            yield return null;
        }
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void StartGame()
    {
        // Debug.Log("We are here");
        // AudioManager.Instance.GetSoundEventInstance(AudioManager.Sounds.MainLoop).setParameterByName("Intensity", 0);
        // player.transform.position = room1SpawnPoint.position;
        // LoadScene(Scenes.EscapeRoom);
        // StartCoroutine(WaitForSceneLoad((int)Scenes.EscapeRoom));
        // AudioManager.Instance.PlaySound(AudioManager.Sounds.MainLoop);
        // gameTimer = 0f;
        // Invoke("StupidThing", 2);
        // TeleportManager.Instance.ActivateTeleportation(true);



    }

    public void StupidThing()
    {
        // You might be asking yourself WHYYYY??! and my answer is -> because it fixes a stupid bug i couldnt fix in any other awy.
        
        RightHand.enabled = false;
        RightHand.enabled = true;
        LeftHand.enabled = false;
        LeftHand.enabled = true;
        
    }
    
    IEnumerator WaitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }
    }

    private void Update()
    {
        
    }

    public void Restart()
    {
        TeleportManager.Instance.ActivateTeleportation(false);
        // AudioManager.Instance.StopSound(AudioManager.Sounds.MainLoop);
        // player.transform.position = roomMainSSpawnPoint.position;
        // LoadScene(Scenes.MainMenu);
    }

    public void Win()
    {
        // AudioManager.Instance.PlaySound(AudioManager.Sounds.Win);
    }

    public void NextLevel()
    {
        SetupReady = false;
        gameState += 1;
        LevelManager.instance.LevelSetup(gameState);
        trash.firstEncounter = true;
    }
}