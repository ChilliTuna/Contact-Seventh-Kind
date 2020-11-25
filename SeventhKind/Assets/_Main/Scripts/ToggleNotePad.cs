using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleNotePad : MonoBehaviour
{
    public GameObject text;
    public GameObject book;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        book.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           toggle = toggle ? false : true;
        }
        text.SetActive(toggle);
        book.SetActive(toggle);
    }
}
