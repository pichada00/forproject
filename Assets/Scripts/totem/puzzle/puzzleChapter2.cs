using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleChapter2 : MonoBehaviour
{
    public Darktotem darktotem;
    public Puzzlemanager puzzlemanager;
    public int index;

    //public string functionRestore = darktotem.restoreTotemForPuzzle();

    private void Awake()
    {
        darktotem = GetComponent<Darktotem>();
        puzzlemanager = GameObject.Find("Puzzlemanager").GetComponent<Puzzlemanager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "weapon")
        {
            puzzlemanager.indexForDestroyTotem++;
            if(index != puzzlemanager.indexForDestroyTotem)
            {
                puzzlemanager.indexForDestroyTotem = 0;
                StartCoroutine("enumerator");
            }
        }
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5.0f);

        darktotem.restoreTotemForPuzzle();
        if(darktotem.light.range >= 30)
        {
            darktotem.Dome.SetActive(true);
            StopCoroutine("enumerator");
        }
    }


}
