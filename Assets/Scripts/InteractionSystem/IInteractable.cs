using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum interactsomething { handright,handleft,handboth,mouseleft,mouseright }
public interface IInteractable
{
    public interactsomething interactsomething { get; }
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor);
    public bool InteractL(Interactor interactor);
    public bool InteractR(Interactor interactor);
}
