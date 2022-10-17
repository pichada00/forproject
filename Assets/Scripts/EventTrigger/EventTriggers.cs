using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTriggers : MonoBehaviour
{
    public UnityEvent onTrigger;

    public bool destroyAfterTrigger = true;

    private void Awake()
    {
        if(onTrigger == null)
        {
            onTrigger = new UnityEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();
        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }
}
