using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLightTotem : MonoBehaviour,IInteractable
{
    public int countTotemdevilDestroy = 0;
    public interactsomething thisThing;
    public Light light;
    bool passCheckpoint;
    public interactsomething interactsomething => thisThing;

    public string InteractionPrompt => throw new System.NotImplementedException();

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public bool InteractL(Interactor interactor)
    {
        if(countTotemdevilDestroy == 3)
        {
            light.enabled = !light.enabled;
        }
        return true;
    }

    public bool InteractR(Interactor interactor)
    {
        if (countTotemdevilDestroy == 3)
        {
            light.enabled = !light.enabled;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(passCheckpoint == true)
        {
            countTotemdevilDestroy = 3;
        }
    }
}
