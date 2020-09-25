using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfWritingNotepad : MonoBehaviour
{
    public TextMeshPro tm;
    TextMeshProUGUI tmm;
    string Text;
    // Start is called before the first frame update
    void Start()
    {
        tmm = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Text += "W";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Text += "A";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Text += "S";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Text += "D";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Text += " ";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Text += "\n";
        }
        tmm.text = Text;
    }
}
