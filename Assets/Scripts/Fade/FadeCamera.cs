using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCamera : MonoBehaviour
{
    public GameObject[] cameraObjects;
    public RawImage blackSceenInCanvas;
    public float valueAlpha = 0;
    public float speedValueAlpha;
    bool checkCamera = false;
    int index = 0;
    //public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeIn(float time)
    {
        if(checkCamera == true)
        {
            cameraObjects[index].SetActive(false);
        }

        if (valueAlpha <= 254)
        {
            blackSceenInCanvas.color = new Color32(0, 0, 0, (byte)valueAlpha);
            valueAlpha += speedValueAlpha * Time.deltaTime;
        }
        else
        {
            StartCoroutine("changeCamera", time);
        }        
    }

    IEnumerator changeCamera(float time)
    {
        yield return new WaitForSeconds(time);
        if (valueAlpha > 0)
        {
            blackSceenInCanvas.color = new Color32(0, 0, 0, (byte)valueAlpha);
            valueAlpha -= speedValueAlpha * Time.deltaTime;
        }
        checkCamera = true;
        cameraObjects[index].SetActive(checkCamera);
        if(index < cameraObjects.Length)
        {
            fadeIn(time);
        }

        Debug.Log("changeCamera");
    }
}
