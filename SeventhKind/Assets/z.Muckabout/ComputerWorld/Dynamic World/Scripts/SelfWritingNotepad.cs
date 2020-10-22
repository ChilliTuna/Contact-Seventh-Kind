using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfWritingNotepad : MonoBehaviour
{
    public TextMeshProUGUI tm;
    string Text;
    // Start is called before the first frame update
    void Start()
    {
        tm = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Text += "This is a test one. \n";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Text += "This is a test two. \n";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Text += "This is a test three. \n";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Text += "This is a test four. \n";
        }
        tm.text = Text;
        
    }
}
