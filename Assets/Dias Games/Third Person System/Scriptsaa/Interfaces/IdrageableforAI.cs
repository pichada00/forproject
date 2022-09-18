using UnityEngine;

public interface IDIdrageableforAIraggable : IhandiktoAI, ICharacterTargetPos
{
    void StartDrag();
    void StopDrag();

    bool Move(Vector3 velocity);
}

