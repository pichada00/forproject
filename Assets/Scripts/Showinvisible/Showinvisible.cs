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

    [SerializeField] private bool isLoaded;
    [SerializeField] private bool shouldLoad;

    //add
    public MeshRenderer renderers;
    public LighterSystem lighterL;
    public LighterSystem lighterR;
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
        lighterL = GameObject.Find("lamb position L").GetComponent<LighterSystem>();
        lighterR = GameObject.Find("lamb position R").GetComponent<LighterSystem>();
        meshCollider = this.gameObject.GetComponent<MeshCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        //Material[] mats = renderers.materials;

        if (lighterL.openlamb == true || lighterR.openlamb == true)
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

            Material[] mats = renderers.materials;
            mats[0].SetFloat("Dissolve", 1);
            renderers.material = mats[0];
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
            mats[0].SetFloat("Dissolve", Mathf.Sin(t * speed));
            t += Time.deltaTime;
            if (Mathf.Sin(t * speed) <= 0) { mats[0].SetFloat("Dissolve", 0); }
            renderers.material = mats[0];
            isLoaded = true;
            meshCollider.enabled = true;

        }
    }

    void UnLoadScene()
    {
        if (shouldLoad == false)
        {
            Material[] mats = renderers.materials;
            mats[0].SetFloat("_Cutoff", 1);
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
