using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiasGames.Components;


public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    public bool hasRegenerated = true;
    public bool weAreSprinting = false;
    public bool weAreJumping = false;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)] [SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)] [SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;

    public Mover mover;
    
    public LighterSystem lighter;

    private float useLampRun; 

    private void Start()
    {
        mover = GetComponent<Mover>();
        useLampRun = staminaDrain * 1f;
    }
    private void Awake()
    {
        lighter = GameObject.Find("Lamp").GetComponent<LighterSystem>();
    }

    private void Update()
    {
        if (!weAreSprinting)
        {
            if(playerStamina <= maxStamina - 0.01)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina();

                if (playerStamina >= maxStamina)
                {
                    
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated && Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;

            if (lighter.openlamb == true)
            {
                playerStamina -= useLampRun * Time.deltaTime;
            }

            UpdateStamina();


            if (playerStamina <= 0)
            {
                hasRegenerated = false;
                mover.isRun = false;
            }
            
            if(playerStamina >= 0)
            {
                Invoke("ChangeBoolRegen",0.1f);
            }
            
        }
    }

    void UpdateStamina()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
    }

    private void ChangeBoolRegen()
    {
        if(hasRegenerated == false)
        {
            hasRegenerated = true;
            return;
        }
    }
    
}
