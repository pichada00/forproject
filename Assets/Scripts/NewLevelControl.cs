using System.Collections;
using DiasGames.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float delayToRestartLevel;
    
    private NewCheckPoint _newCheckPoint;

    private Health _playerHealth;

    private bool _isRestartingLevel;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        _playerHealth = player.GetComponent<Health>();
        _newCheckPoint = GetComponent<NewCheckPoint>();
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
        
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private IEnumerator OnRestart()
    {
        _isRestartingLevel = true;

        yield return new WaitForSeconds(delayToRestartLevel);
        
        player.transform.position = _newCheckPoint.currentPlayerCheckPoint.position;

        _isRestartingLevel = false;
    }
}
