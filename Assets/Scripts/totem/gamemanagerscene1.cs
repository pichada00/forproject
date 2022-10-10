using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanagerscene1 : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.sceneInfostage1.totemDestroyed = GameObject.FindGameObjectsWithTag("totemlight");

        GameManager.Instance.sceneInfostage1.checkpointed = GameObject.FindGameObjectsWithTag("CheckPoint");

        for(int i = 0; i < GameManager.Instance.sceneInfostage1.counttotemdestroy; i++)
        {
            GameManager.Instance.sceneInfostage1.totemDestroyed[i].SetActive(false);
        }

        for (int i = 0; i < GameManager.Instance.sceneInfostage1.countcheckpointed; i++)
        {
            GameManager.Instance.sceneInfostage1.checkpointed[i].SetActive(false);
        }
    }
}
