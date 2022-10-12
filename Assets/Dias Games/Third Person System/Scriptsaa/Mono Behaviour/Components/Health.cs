using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DiasGames.Components
{
    public class Health : MonoBehaviour, IDamage
    {
        [SerializeField] private float MaxHealthPoints = 175;
        [Space]
        [SerializeField] private UnityEvent OnCharacterDeath;

        //add
        [Range(0, 50)] [SerializeField] private float healthDrain = 15f;
        [Range(0, 50)] [SerializeField] private float healthRegen = 15f;

        [SerializeField] public string animtestDieState = "Die";
        [SerializeField] public string animtestReturnState = "Ground.Idle";

        // internal vars
        [SerializeField] private float _currentHP = 175;

        public float CurrentHP { get { return _currentHP; } }
        public float MaxHP { get { return MaxHealthPoints; } }

        public event Action OnHealthChanged;
        public event Action OnDead;
        public event Action OnDead1;

        public event Action onRestart;

        //add
        public LighterSystem lighterL;
        public LighterSystem lighterR;
        public invisibleAI invisible;
        public Transform aiBuddy;
        public Animator animator;

        [SerializeField] private RawImage blurdis;
        [SerializeField] private float alphascore;
        [SerializeField] private bool distance;

        public bool dead=false;
        private void Start()
        {
            _currentHP = MaxHealthPoints;
            OnHealthChanged?.Invoke();
        }

        private void Awake()
        {
            lighterR = GameObject.Find("lamb position R").GetComponent<LighterSystem>();
            lighterL = GameObject.Find("lamb position L").GetComponent<LighterSystem>();
            invisible = GameObject.Find("AI").GetComponent<invisibleAI>();
            aiBuddy = GameObject.Find("AI").GetComponent<Transform>();
            animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
            Gizmos.DrawWireSphere(transform.position, 26);
        }
        //add
        private void Update()
        {
            OverUesLamp();
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                OnDead?.Invoke();
                if(dead == false)
                {

                    testUnityevent();
                }



            }
            if(Vector3.Distance(transform.position,aiBuddy.position) >= 9)
            {
                alphascore += 100 * Time.deltaTime;

                if (alphascore <= 0)
                {
                    alphascore *= -20f;
                }
                if (alphascore >= 200)
                {
                    alphascore = 200;
                }
            } else
            {
                if (alphascore > 0)
                {
                    alphascore -= 25 * Time.deltaTime;
                }

            }

            if (Vector3.Distance(transform.position, aiBuddy.position) >= 26)
            {

                _currentHP-=MaxHP;
            }
            
            blurdis.color = new Color32(255, 255, 255, (byte)alphascore);

        }


        public void testUnityevent()
        {
            dead = true;
            animator.SetBool(animtestDieState, true);
            OnCharacterDeath.Invoke();
        }
        public void Damage(int damagePoints)
        {
            _currentHP -= damagePoints;

            if (_currentHP <= 0)
            {
                _currentHP = 0;
                OnDead?.Invoke();
                OnCharacterDeath.Invoke();
            }

            OnHealthChanged?.Invoke();
        }
        //add
        public void OverUesLamp()
        {
            if ((lighterR.openlamb == true && invisible.currentCutoff > 0) || 
                (lighterL.openlamb == true && invisible.currentCutoff > 0) ||
                invisible.neartotem == true)
            {
                _currentHP -= healthDrain * Time.deltaTime;                
            }
            else if(_currentHP <= MaxHealthPoints - 0.01)
            {
                _currentHP += healthRegen * Time.deltaTime;

            }
            
            OnHealthChanged?.Invoke();
        }

        /// <summary>
        /// Restore an amount of health points
        /// </summary>
        /// <param name="hp">Health points</param>
        public void RestoreHealth(int hp)
        {
            _currentHP += hp;
            if (_currentHP > MaxHealthPoints)
                _currentHP = MaxHealthPoints;

            OnHealthChanged?.Invoke();
        }

        /// <summary>
        /// Restores all character health
        /// </summary>
        public void RestoreFullHealth()
        {
            _currentHP = MaxHealthPoints;

            OnHealthChanged?.Invoke();
        }
    }
}