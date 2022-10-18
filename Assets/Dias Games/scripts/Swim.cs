using DiasGames.Abilities;
using UnityEngine;
using DiasGames.Components;

namespace DiasGames.Abilities
{
    public class Swim : AbstractAbility
    {
        [Header("swim parameters")]
        //[SerializeField] private float speedSwim = 1.2f;
        //[SerializeField] private bool Inwater = false;
        [SerializeField] private Transform origin;

        private IMover _mover = null;
        private void Awake()
        {
            _mover = GetComponent<IMover>();
            /*_damage = GetComponent<IDamage>();
            _audioPlayer = GetComponent<CharacterAudioPlayer>();
            _camera = Camera.main.transform;*/
        }
        public override void OnStartAbility()
        {
            //if ()
              //  PerformSwim()
            throw new System.NotImplementedException();
        }

        public override bool ReadyToRun()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateAbility()
        {
            throw new System.NotImplementedException();
        }

        private void PerformSwim()
        {

        }

    } 
}
