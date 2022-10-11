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
    public bool arriveStage2;
    public bool arriveStage3;
    public bool arriveStage4;
    public bool arriveStage5;
    
    public Transform playerPosition;
    public Transform aiPosition;
    public Transform currentPlayerCheckPoint;
    public Transform currentAICheckPoint;

    //public Transform first position stage1;

    //public NewCheckPoint newCheckPoint;

    [SerializeField] public SceneInfo sceneInfostage0;//scriptsAble object
    [SerializeField] public SceneInfo sceneInfostage1;//scriptsAble object
    [SerializeField] public SceneInfo sceneInfostage2;//scriptsAble object
    [SerializeField] public SceneInfo sceneInfostage3;//scriptsAble object
    [SerializeField] public SceneInfo sceneInfostage4;//scriptsAble object
    [SerializeField] public SceneInfo sceneInfostage5;//scriptsAble object


    [SerializeField] public GameObject playergameObject;

    [SerializeField] public int coubtPassStage = 0;
    [SerializeField] public bool continueGa = false;

    [SerializeField] public string _currentScene;

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

        //newCheckPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCheckPoint>();
        //playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (coubtPassStage >= 1)
        {
            firstPlay = false;
        }
        else
        {
            firstPlay = true;
        }
    }

    public void NewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        sceneInfostage1.counttotemdestroy = 0;
        sceneInfostage2.counttotemdestroy = 0;
        sceneInfostage3.counttotemdestroy = 0;
        sceneInfostage4.counttotemdestroy = 0;
        sceneInfostage5.counttotemdestroy = 0;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        _currentScene = sceneName;
        Debug.Log(_currentScene);
        switch (sceneName)
        {
            case "Stage1Chapter1":
                sceneInfostage1.countcheckpointed = 0;
                sceneInfostage1.counttotemdestroy = 0;
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

    public void continueGame()
    {
        continueGa = true;
        switch (coubtPassStage)
        {
            case 1:
                ChangeScene("Stage1Chapter1");
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
