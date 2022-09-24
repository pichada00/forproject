using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckMethodOff
{
    Distance,
    Trigger
}
public class OffInvisible : MonoBehaviour
{
    public Transform player;
    public CheckMethodOff checkMethodOff;
    public float loadRange;

    [SerializeField] private bool isLoaded;
    [SerializeField] private bool shouldLoad;

    //add
    public MeshRenderer renderers;
    public LighterSystem lighter;
    public Collider collider;

    public float t = 3f;
    public float speed = 0.5f;

    private MeshCollider meshCollider;

    private void Awake()
    {
        player = GameObject.Find("CS Character Controller").transform;
        renderers = this.gameObject.GetComponent<MeshRenderer>();
        collider = this.gameObject.GetComponent<Collider>();
        lighter = GameObject.FindGameObjectWithTag("Lamp").GetComponent<LighterSystem>();
        meshCollider = this.gameObject.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lighter.openlamb == true)
        {
            Debug.Log(Mathf.Sin(t * speed));
            collider.enabled = true;
            if (checkMethodOff == CheckMethodOff.Distance)
            {
                DistanceCheck();
            }
            else if (checkMethodOff == CheckMethodOff.Trigger)
            {
                TriggerCheck();
            }
        }
        else
        {
            collider.enabled = false;
            meshCollider.enabled = true;
            
        }
    }
    void DistanceCheck()
    {
        
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            if (!isLoaded)
            {
                LoadScene();
            }
        }
        else
        {
            UnLoadScene();
        }
    }
    void TriggerCheck()
    {
        
        if (shouldLoad)
        {
            LoadScene();
        }
        else 
        {
            UnLoadScene();
        }
    }
    void LoadScene()
    {
        if (shouldLoad)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", 1);
            renderers.material = mats[0];
            isLoaded = true;
            meshCollider.enabled = false;
        }
    }
    void UnLoadScene()
    {
        if (shouldLoad == false)
        {

            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", 0);
            renderers.material = mats[0];


        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;

        }
    }
}
