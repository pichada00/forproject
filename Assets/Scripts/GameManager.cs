using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    [SerializeField] private GameManager gameManager;
    public bool firstPlay = true;
    public bool arriveStage2 = false;
    public bool arriveStage3 = false;
    public bool arriveStage4 = false;
    public bool arriveStage5 = false;
    
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

    public void NewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        switch (sceneName)
        {
            case "0":
                break;
            case "1":
                sceneInfostage1.currentCheckPointOfStageOne = new Vector3(45, 1, -5);
                break;
            case "2":
                sceneInfostage2.currentCheckPointOfStageOne = new Vector3(0, 0, 0);
                break;
            case "3":
                sceneInfostage3.currentCheckPointOfStageOne = new Vector3(0, 0, 0);
                break;
        }
        
    }

    public void RestartPlayerCheckpoint()
    {
        playerPosition.position = sceneInfostage1.currentCheckPointOfStageOne;
    }
}
