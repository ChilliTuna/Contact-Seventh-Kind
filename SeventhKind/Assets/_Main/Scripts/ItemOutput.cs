using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemOutput : MonoBehaviour
{
    public UnityEvent onFirstInteract;
    public UnityEvent onInteract;

    private bool hasInteracted;

    public void Interact()
    {
        if (!hasInteracted)
        {
            onFirstInteract.Invoke();
        }
        onInteract.Invoke();
    }
}
