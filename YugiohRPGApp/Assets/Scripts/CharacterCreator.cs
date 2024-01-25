using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
/*
Created by: Matthew Sahakian
Oct. 11 2023
Overview: Generates a character.
--------------------------------------------------------------------------------------------------------
 */
public class CharacterCreator : GameScreen
{
    private Character newCharacter;
    private string newName;
    private int newClass;
    //private int newLevel;

    public TMPro.TMP_InputField _input;
    public TMP_Dropdown classDropDown;
    public Button continueBtn;

    public override void Initialize()
    {
        //make sure we have a class value if none is selected.
        newClass = classDropDown.value;
        newClass = 1;
        
        //generate a list of options from the database.
        List<string> dropdownClassList = new List<string>();

        //create a new dictionary from the database to serve as our iterated dictionary and grab values.
        var classDictionary = ClassDatabase._gameClass.Where(kvp => kvp.Value is SimpleSQL.Demos.GameClass).Select(kvp => new { kvp.Key, Value = (SimpleSQL.Demos.GameClass)kvp.Value });
        
        foreach (var _class in classDictionary) 
        {
            dropdownClassList.Add(_class.Value.Name);
        }

        classDropDown.AddOptions(dropdownClassList);
        
    }

    public void Awake()
    {
        gameFSM = GetComponent<GameFSM>(); 
        main = GetComponent<Main>();
        //exitBtn = GetComponent<Button>();
        
        Initialize();
    }
    //when the character creator is selected enable the screen.
    private void OnEnable()
    {
        window.SetActive(true);
        exitBtn.onClick.AddListener(BackButtonPress);
        main.enabled = false;
    }

    private void OnDisable()
    {
       
    }

    public void EmptyCharacter() 
    {
        newCharacter = new Character();
    } //Unused cleanup code.

    public void CreateNewCharacter(string val) //Generates a new character and accepts a name input. (Step 1)
    {
        if (val == "") 
        { return; }
        newName = val;
    }

    //create a method for selecting your class.
    public void ChooseClass(int val) //chooses a class from the dropdown and cashes its value as the newcharacters class value. (Step 2)
    {
        if (newCharacter == null) { return; }
        newClass = val +1 ;//this is a problimatic line it works by adding one to the selected value by the dropdown.
        continueBtn.enabled = true;
       
    }

    public void ConfirmationButton() //Assigns the prior steps to a newly generated character. Adds new character to MAIN.Characters list.
    {
        //Confirm fields are correct
        if (newCharacter == null) //keep the index within bounds or return
        {
            print("Character was null");
            //To do: write to the text manager and inform the player to select their values.
            return;
        }
        if (newName == null) 
        {
            TextManager.Instance.QueMessage("Character name not entered");
            return; 
        }
        /*bool result1 = newName.All(Char.IsLetter); //This block of code was replaced by the input setting.
        if (!result1) 
        {
            TextManager.Instance.QueMessage("Name Must Contain Letters");
            return;
        }
        */

        newCharacter.CasheACharacterClass(newClass);
        newCharacter.WriteCharacterName(newName);
        
        TextManager.Instance.QueMessage("Character added: " + newCharacter.ReadCharacterName() + ". " + "Character Level is " + newCharacter.IncreaseEXP(0).ToString() + " Class name is: " + newCharacter.myClasses[0].className.ToString());
        main.Characters.Add(newCharacter);
        if (main.playerData != null)
        {
            main.playerData.charList.Add(newCharacter);
        }
        else 
        {
            Debug.Log("PlayerDataNull");
            main.playerData = new PlayerDataModel();
            main.playerData.charList.Add(newCharacter);
        }
        _input.text = "";
        StartCoroutine(CleanUpExit(2));
        //Add the newCharacter to the list of characters.
    }
}
