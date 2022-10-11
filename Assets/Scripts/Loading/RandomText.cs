using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomText : MonoBehaviour
{
    public int randNum;
    public GameObject randText;
    public bool genText = false;


    void Start()
    {
        StartCoroutine(TextTracker());
        randText.GetComponent<Animator>().Play("RandomText");
    }
    /*
    void Update()
    {
        if (genText == false)
        {
            randText.GetComponent<Animator>().Play("New State");
            genText = true;
            StartCoroutine(TextTracker());
        }
    }
    */

    IEnumerator TextTracker()
    {
        randNum = Random.Range(1, 4);
        if (randNum == 1)
        {
            randText.GetComponent<TextMeshProUGUI>().text = "1";
        }
        if (randNum == 2)
        {
            randText.GetComponent<TextMeshProUGUI>().text = "2";
        }
        if (randNum == 3)
        {
            randText.GetComponent<TextMeshProUGUI>().text = "3";
        }
        //randText.GetComponent<Animator>().Play("RandomText");
        yield return new WaitForSeconds(9);
        //genText = false;
    }
}
