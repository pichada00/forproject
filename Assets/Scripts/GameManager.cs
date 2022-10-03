using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    [SerializeField] private GameManager gameManager;
    public bool firstplay = true;
    public bool passStage2 = false;
    public bool passStage3 = false;
    public bool passStage4 = false;
    public bool passStage5 = false;
    
    public Transform playerPosition;
    public Transform aiPosition;
    public Transform currentPlayerCheckPoint;
    public Transform currentAICheckPoint;

    //public Transform firstpositionstage1;

    public NewCheckPoint newCheckPoint;

    [SerializeField] public SceneInfo sceneInfostage1;//scripts able object
    [SerializeField] public SceneInfo sceneInfostage2;//scripts able object
    [SerializeField] public SceneInfo sceneInfostage3;//scripts able object
    [SerializeField] public SceneInfo sceneInfostage4;//scripts able object
    [SerializeField] public SceneInfo sceneInfostage5;//scripts able object

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        else if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        newCheckPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCheckPoint>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        
    }

    public void NewGame(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
        switch (scenename)
        {
            case "0":
                break;
            case "1":
                sceneInfostage1.currentCheckPointOfStageOne = new Vector3(0, 0, 0);
                break;
            case "2":
                sceneInfostage2.currentCheckPointOfStageOne = new Vector3(0, 0, 0);
                break;
        }
        
    }

    public void RestartPlayerCheckpoint()
    {
        playerPosition.position = sceneInfostage1.currentCheckPointOfStageOne;
    }
}
