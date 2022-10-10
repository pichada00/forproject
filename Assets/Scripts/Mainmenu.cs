using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public Button ContinueBtn;

    private void Update()
    {
        if(GameManager.Instance.firstPlay == false)
        {
            ContinueBtn.interactable = true;
        }else
        {
            ContinueBtn.interactable = false;
        }
    }
}
