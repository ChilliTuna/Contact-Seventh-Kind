using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UniversalFunctions : MonoBehaviour
{
    public void DeactivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void SwitchActiveState(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
