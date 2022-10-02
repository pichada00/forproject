using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LighterSystem : MonoBehaviour
{
    public static LighterSystem instance;
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    //public Slider StaminaSlider;
    public Light light;
    public bool openlamb = false;
    public keep keepcode;
    //private StarterAssetsInputs _input;
    //public GameObject particleLight;
    //public static LighterSystem lighterSystem;

    public LayerMask aiBuddy;
    private readonly Collider[] _colliders = new Collider[1];
    public float radius;
    public int indexAIBuddy = 0;

    private void Awake()
    {
      //  lighterSystem = this;
    }
    void Start()
    {
        
        //_input = GetComponent<StarterAssetsInputs>();
        light = GetComponent<Light>();
        keepcode = GetComponent<keep>();
        Stamina = MaxStamina;
        //StaminaSlider.GetComponent<Slider>().maxValue = MaxStamina;
        //StaminaSlider.GetComponent<Slider>().value = Stamina;
    }
    void Update()
    {
        //StaminaSlider.GetComponent<Slider>().value = Stamina;

        //inputsystem
        if(keepcode.keeped == true)
        {
            if(keepcode.left == true)
            {
                if (Input.GetMouseButtonDown(0) && openlamb == false )
                {
                    light.range = 20.00f;
                    openlamb = true;
                    //particleLight.gameObject.SetActive(true);
                    return;                    
                }
                if (Input.GetMouseButtonDown(0) && openlamb == true)
                {
                    light.range = 0.00f;
                    openlamb = false;
                    //particleLight.gameObject.SetActive(false);
                    return;
                    
                }
            }else if (keepcode.right)
            {
                if (Input.GetMouseButtonDown(1) && openlamb == false )
                {
                    light.range = 20.00f;
                    openlamb = true;
                    //particleLight.gameObject.SetActive(true);
                    return;
                    
                }
                if (Input.GetMouseButtonDown(1) && openlamb == true)
                {
                    light.range = 0.00f;
                    openlamb = false;
                    //particleLight.gameObject.SetActive(false);
                    return;
                    
                }
            }
            
        }
        else
        {
            openlamb = false;
            light.range = 0.00f;
        }
        if(openlamb == true)
        {
            checkAI();
        }

        
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void checkAI()
    {
        Collider[] hitsMonster = Physics.OverlapSphere(transform.position, radius, aiBuddy, QueryTriggerInteraction.Ignore);
        indexAIBuddy = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, aiBuddy);
        if (indexAIBuddy > 0)
        {
            var arrayMons = _colliders[0].GetComponent<invisibleAI>();
            float distanceAILamp = Vector3.Distance(transform.position, arrayMons.transform.position);
            if (arrayMons.currentCutoff < 1)
            {
                arrayMons.CutofFromLighter = 0;
            }            
            if (distanceAILamp < radius && arrayMons.follow == false)
            {
                Debug.Log(distanceAILamp);
                arrayMons.CutofFromLighter = (distanceAILamp / radius) - 1f;
                if(arrayMons.CutofFromLighter < 0)
                {
                    arrayMons.CutofFromLighter *= -3.0f;
                }
            }
        }
    }

    public IEnumerator RemoveStamina(float value, float time)
    {
        yield return new WaitForSeconds(time);
        if (Stamina > 0)
        {
            Stamina -= value;
        }
    }
    public void GainStamina(float value)
    {
        Stamina += value;
        if (Stamina > MaxStamina)
        {
            Stamina = MaxStamina;
        }
    }

    public IEnumerator RecoveryStamina(float value,float time)
    {
        yield return new WaitForSeconds(time);
        Stamina += value;
    }
}
