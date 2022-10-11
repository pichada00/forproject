using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanagerscene1 : MonoBehaviour
{
    public GameObject[] _totemLight;
    public GameObject[] _checkpointed;
    private void Awake()
    {
        _totemLight = GameObject.FindGameObjectsWithTag("totemlight");

        _checkpointed = GameObject.FindGameObjectsWithTag("CheckPoint");

        for(int i = 0; i < GameManager.Instance.sceneInfostage1.counttotemdestroy; i++)
        {
            _totemLight[i].SetActive(false);
        }

        for (int i = 0; i < GameManager.Instance.sceneInfostage1.countcheckpointed; i++)
        {
            _checkpointed[i].SetActive(false);
        }
    }
}
