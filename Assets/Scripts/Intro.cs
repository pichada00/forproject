using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private float wait = 8;
    [SerializeField] private string nameScene;
    private void Start()
    {
        StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(nameScene);
    }
}
