using DiasGames.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hittotem : MonoBehaviour
{

    [SerializeField] private AudioClip clipshittotem;
    [SerializeField] private AudioSource hittedSource;

    private CharacterAudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = GameObject.Find("CS Character Controller").GetComponent<CharacterAudioPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "totemlight")
        {
            if(_audioPlayer)
                _audioPlayer.PlayVoice(clipshittotem);

        }
    }

    public void PlayVoice(AudioClip clip)
    {
        Debug.Log("playsound");
        if (hittedSource == null) return;

        Debug.Log("playsound");
        hittedSource.clip = clip;
        hittedSource.Play();
    }
}
