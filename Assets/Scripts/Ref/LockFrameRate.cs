using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFrameRate : MonoBehaviour
{
    [SerializeField] public int frameRate;

    public void FrameRate()
    {
        Application.targetFrameRate = frameRate;
    }
}
