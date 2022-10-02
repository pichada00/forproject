using System;
using UnityEngine;
using UnityEngine.Events;

namespace DiasGames.Components
{
    public class Health : MonoBehaviour, IDamage
    {
        [SerializeField] private float MaxHealthPoints = 100;
        [Space]
        [SerializeField] private UnityEvent OnCharacterDeath;

        //add
        [Range(0, 50)] [SerializeField] private float healthDrain = 0.05f;
        [Range(0, 50)] [SerializeField] private float healthRegen = 0.25f;

        // internal vars
        [SerializeField] private float _currentHP = 100;

        public float CurrentHP { get { return _currentHP; } }
        public float MaxHP { get { return MaxHealthPoints; } }

        public event Action OnHealthChanged;
        public event Action OnDead;

        //add
        public LighterSystem lighter;
        public invisibleAI invisible;

        private void Start()
        {
            _currentHP = MaxHealthPoints;
            OnHealthChanged?.Invoke();
        }

        private void Awake()
        {
            lighter = GameObject.FindGameObjectWithTag("Lamp").GetComponent<LighterSystem>();
            invisible = GameObject.Find("AI").GetComponent<invisibleAI>();
        }

        //add
        private void Update()
        {
            OverUesLamp();
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                OnDead?.Invoke();
                OnCharacterDeath.Invoke();
            }
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
            if ((lighter.openlamb == true && invisible.currentCutoff > 0) || invisible.neartotem == true)
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