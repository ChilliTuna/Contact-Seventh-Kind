using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float viewDistance;
    public float viewHeight;
    public float xOffset;

    [HideInInspector]
    public GameObject currentPlayer;
}
