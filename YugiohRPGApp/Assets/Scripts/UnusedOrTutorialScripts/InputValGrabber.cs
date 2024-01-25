using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
Created by: Matthew Sahakian
date: Oct.11.23
Overview: GrabInformation from the input window to use
Youtube tutorial. Link: https://www.youtube.com/watch?v=zahrwl1125k&t=200s
 */
public class InputValGrabber : MonoBehaviour
{

    [SerializeField] private string inputText;

    [SerializeField]private GameObject reactionGroup;
    [SerializeField]private TMP_Text reactionTextBox;

    public void GrabFromInputField(string input) 
    {
        inputText = input;
        DisplayReactionToInput();
    }

    void DisplayReactionToInput() 
    {
        reactionTextBox.text = "Welcome to the team, " + inputText + "!";
        reactionGroup.SetActive(true);
    }
   
    
}
