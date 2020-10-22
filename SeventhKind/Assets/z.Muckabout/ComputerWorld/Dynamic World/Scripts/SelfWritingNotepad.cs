using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfWritingNotepad : MonoBehaviour
{
    public TextMeshProUGUI tm;
    string text;

    private void Update()
    {
        text = tm.text;
    }

    public void UpdateNotepad(string newString)
    {
        text += ("\n" + newString);
        tm.text = text;
    }
}
