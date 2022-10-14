using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToNextLevel()
    {
        animator.SetBool("fadeout",true);
        Invoke("FadeAgain", 1.5f);
    }
    public void FadeAgain()
    {
        Debug.Log("fadein");
        animator.SetBool("fadeout", false);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
