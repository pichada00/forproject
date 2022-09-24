using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckMethod
{
    Distance,
    Trigger
}
public class Showinvisible : MonoBehaviour
{
    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    [SerializeField]private bool isLoaded;
    [SerializeField] private bool shouldLoad;

    //add
    public MeshRenderer renderers;
    public LighterSystem lighter;
    public Collider collider;

    public float t = 3f;
    public float speed = 0.5f;

    private MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        
                                            
    }

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
        //Material[] mats = renderers.materials;

        if (lighter.openlamb == true)
        {
            Debug.Log(Mathf.Sin(t * speed));
            collider.enabled = true;
            if (checkMethod == CheckMethod.Distance)
            {
                DistanceCheck();
            }
            else if (checkMethod == CheckMethod.Trigger)
            {
                TriggerCheck();
            }
        }
        else
        {
            collider.enabled = false;
            meshCollider.enabled = false;
        }
        
    }

    

    void DistanceCheck()
    {
        //Checking if the player is within the range
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
        //shouldLoad is set from the Trigger methods
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
            Debug.Log(Mathf.Sin(t * speed));
            Material[] mats = renderers.materials;
            //renderers.gameObject.SetActive(true);
            mats[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
            t += Time.deltaTime;
            if (Mathf.Sin(t * speed) <= 0) { mats[0].SetFloat("_Cutoff", 0); }
            renderers.material = mats[0];
            isLoaded = true;
            meshCollider.enabled = true;

        } 
    }

    void UnLoadScene()
    {     
        if(shouldLoad == false)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", 1);
            renderers.material = mats[0];

        }
        /*{
            Material[] mats = renderers.materials;
        
            mats[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
            t += Time.deltaTime;
            if (Mathf.Sin(t * speed) >= 0.9f) 
            {
                mats[0].SetFloat("_Cutoff", 1);
                renderers.gameObject.SetActive(false);
            }
            renderers.material = mats[0];
            isLoaded = false;
        }*/

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
