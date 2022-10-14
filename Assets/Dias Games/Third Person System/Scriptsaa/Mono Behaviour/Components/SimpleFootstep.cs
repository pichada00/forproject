using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiasGames.Components
{

    public class SimpleFootstep : MonoBehaviour
    {
        [SerializeField] private AudioSource footstepAudioSource;

        public void Footstep(AnimationEvent evt)
        {
            if (evt.animatorClipInfo.weight > 0.5f)
                playSound(evt, "walk");
        }
        public void playSound(AnimationEvent evt, string animation)
        {
            switch (animation)
            {
                case "walk":
                    AudioManager.Instance.PlaySFX("Walk");
                    break;
            }

        }

        public void Climbstep(AnimationEvent evt)
        {
            if (evt.animatorClipInfo.weight > 0.5f)
                playSound(evt, "climb");
        }
    }
}