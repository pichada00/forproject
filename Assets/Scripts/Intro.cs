using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("MainMenuUI");
    }
}
