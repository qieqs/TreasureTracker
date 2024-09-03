using UnityEngine;
using UnityEditor;
using System;
using System.Collections; 
public class GUI_Explainer : MonoBehaviour
{
    public string stringToEdit = "Hello World\nI've got 2 lines...";

    void OnGUI()
    {
        // Make a multiline text area that modifies stringToEdit.
        stringToEdit = GUI.TextArea(new Rect(10, 10, 200, 100), stringToEdit, 200);

    }
}
