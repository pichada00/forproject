using System.Collections;
using DiasGames.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float delayToRestartLevel;

    public GameObject currentCheckPoint;

    private Health _playerHealth;

    private bool _isRestartingLevel;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        _playerHealth = player.GetComponent<Health>();
    }

    private void OnEnable()
    {
        _playerHealth.OnDead += RestartLevel;
    }

    private void OnDisable()
    {
        _playerHealth.OnDead -= RestartLevel;
    }

    private void RestartLevel()
    {
        if (!_isRestartingLevel) 
            StartCoroutine(OnRestart());
        player.transform.position = currentCheckPoint.transform.position;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(name);
    }

    private IEnumerator OnRestart()
    {
        _isRestartingLevel = true;

        yield return new WaitForSeconds(delayToRestartLevel);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _isRestartingLevel = false;
    }
}
