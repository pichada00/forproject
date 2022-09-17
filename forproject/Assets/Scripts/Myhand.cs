using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myhand : MonoBehaviour
{
    public static Myhand instance;

    public bool interactL;
    public bool interactR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool setInteractL(bool check)
    {
        interactL = check;
        return check;
    }

    public bool setInteractR(bool check)
    {
        interactL = check;
        return check;
    }
}
