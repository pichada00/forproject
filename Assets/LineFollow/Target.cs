using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform lockPoint = null;

    public bool isHovered = false;
        
    public void OnHover()
    {
        if (!isHovered)
        {
            isHovered = true;
        }
    }
        
    public void OnHoverLost()
    {
        if (isHovered)
        {
            isHovered = false;
        }
    }
}
