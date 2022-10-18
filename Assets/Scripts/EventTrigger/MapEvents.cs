using UnityEngine;

public class MapEvents : MonoBehaviour
{
    public GameObject rock;
    public void PrintText()
    {
        Debug.Log("Player is Trigger");

    }

    public void RockFalling()
    {
        rock.gameObject.SetActive(true);

    }
}
