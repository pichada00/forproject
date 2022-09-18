using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibleAI : MonoBehaviour
{
    public SkinnedMeshRenderer renderers;
    public LighterSystem lighter;
    public Collider collider;
    float currentCutoff = 0f;

    private void Awake()
    {
        //player = GameObject.Find("CS Character Controller").transform;
        renderers = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
        lighter = GameObject.FindGameObjectWithTag("Lamp").GetComponent<LighterSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lighter.openlamb == true)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            mats[1].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            mats[2].SetFloat("_Cutoff", currentCutoff+=0.5f*Time.deltaTime);
            renderers.material = mats[0];
            renderers.material = mats[1];
            renderers.material = mats[2];
            if (currentCutoff >= 1)
            {
                currentCutoff = 1f;
            }
        }
        else
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            mats[1].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            mats[2].SetFloat("_Cutoff", currentCutoff -= 0.5f * Time.deltaTime);
            renderers.material = mats[0];
            renderers.material = mats[1];
            renderers.material = mats[2];
            if(currentCutoff <= 0)
            {
                currentCutoff = 0f;
            }
        }
    }
}
