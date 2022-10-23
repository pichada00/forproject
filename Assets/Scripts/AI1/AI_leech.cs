using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AI_leech : MonoBehaviour
{
    public SkinnedMeshRenderer renderersBody;
    public SkinnedMeshRenderer renderersEye;
    public float currentCutoff = 0f;
    public float CutofFromLighter = 0f;


    public TypeMonster type;
    public RangeMonster range;
    public Transform totem;
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public State_leech currentState;
    public GameObject projectile;
    public GameObject positionShot = null;
    public bool died = false;
    //TextMeshProUGUI txtStatus;
    //Animator anim;

    public FieldOfView fieldOf;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new Idle_leech(type, range, fieldOf, this.gameObject, agent, player, totem, animator);
    }

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        //txtStatus = this.GetComponentInChildren<TextMeshProUGUI>();
        player = GameObject.Find("AI").GetComponent<Transform>();
        fieldOf = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        //anim.SetInteger("Walk", 1);
        if (died == true)
        {
            Material[] mats = renderersBody.materials;
            Material[] matsEye = renderersEye.materials;
            if (currentCutoff >= 1f)
            {
                currentCutoff = 1f;
                gameObject.SetActive(false);
            }
            mats[0].SetFloat("Dissolve", currentCutoff += 0.02f * Time.deltaTime);
            matsEye[0].SetFloat("Dissolve", currentCutoff += 0.02f * Time.deltaTime);
            renderersBody.material = mats[0];
            renderersEye.material = matsEye[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "weapon")
        {
            switch (type)
            {
                case TypeMonster.normal:
                    Debug.Log("hitmon Normal");
                    currentState = new Stun_leech(type, range, fieldOf, this.gameObject, agent, player, totem, animator);
                    break;
                case TypeMonster.evolve:
                    Debug.Log("hitmon Evolve");
                    currentState = new Died_leech(type, range, fieldOf, this.gameObject, agent, player, totem, animator);
                    break;
            }
            
        }
    }

    public void monsterRangeAttack()
    {
        Debug.Log("RangeAttack");
        Rigidbody rigidbody = Instantiate(projectile, positionShot.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rigidbody.AddForce(agent.transform.forward * 32f, ForceMode.Impulse);
        rigidbody.AddForce(agent.transform.up * 8f, ForceMode.Impulse);
    }

    public void monsterMeleeAttackStart()
    {
        //enable collider
        Debug.Log("meleeAttack");
    }

    public void monsterMeleeAttackEnd()
    {
        //disable collider
        Debug.Log("endmeleeAttack");
    }
}