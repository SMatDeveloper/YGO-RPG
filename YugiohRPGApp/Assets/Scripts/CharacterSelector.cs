using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
Created by Matthew Sahakian
date: Oct.12.2023
Overview: This is yet another app state which will begin with awaiting input from the player about which character from the main.List they want to edit, then itll load the character to the GUI and open buttons for the player to edit their character.
 */

public class CharacterSelector : GameScreen
{
    //public GameObject editorWindow;
    [SerializeField] private TMP_Dropdown _characterListDropdown;
    [SerializeField] private TMP_Dropdown classListDropdown;
    
    TMP_Text tempText;
    int selection = 0;
    //inorder to add an entry you must give the add options a tmp TEXT and SPRITE. I am hopefull the Sprite can be null to start with.


    private void Awake()
    {
        gameFSM = GetComponent<GameFSM>();
        main = GetComponent<Main>();
     
       
    }
    private void OnEnable()
    {
        window.SetActive(true);
        main.enabled = false;
        Initialize();
        exitBtn.onClick.AddListener(BackButtonPress);
        //selection = lookUpID; //keeps the selection from becoming inaccurate when a character is deleted.
    }
    private void OnDisable()
    {
       
    }

    
    public void DropdownChangeVal(int val) 
    {
        selection = val;
    }

    public void CharacterSelect() 
    {
        if (main.Characters.Count <= 0)
        {
            Debug.LogError("Selection was outside the range of the list...");
            return;
        }
        Character selchar = main.Characters[selection];

        TextManager.Instance.QueMessage(selchar.ReadCharacterName().ToString() + " has been selected via the dropdown.");

        lookUpID = selection; //store the lookup id we want.

        //move to the viewer screen.
        StartCoroutine(CleanUpExit(4));

      //this will fire when a character is selected from a dropdown.
      
    }

    public void CharacterDelete() 
    {
        if (main.Characters.Count <= 0)
        {
            print("Selection was outside the range of the list...");
            return;
        }
        //To do: choose an entry on the main.Characters List and remove it from the list.
        TextManager.Instance.QueMessage(main.Characters[selection].ReadCharacterName() + " Is Deleted.");
        main.Characters.RemoveAt(selection);
        main.playerData.charList.RemoveAt(selection);
        selection = lookUpID;

        //Move to the main screen again after the character is deleted.
        StartCoroutine(CleanUpExit(1));
    }


    //This function will check the avaliable characters on mains character list and VISUALLY display them.
    public override void Initialize()
    {
        _characterListDropdown.ClearOptions();
        //creates the list of strings to be loaded. HOWEVER THIS DOESNT REFERENCE ORIGINAL OBJECT.
        List<string> characterNamesList = new List<string>();
        foreach (Character i in main.Characters)
        {
            characterNamesList.Add(i.ReadCharacterName().ToString());
        }
        _characterListDropdown.AddOptions(characterNamesList);

        if (main.Characters.Count <= 1) 
        {
            lookUpID = 0;
            //StartCoroutine(CleanUpExit(4));
        }
        
    }

}
