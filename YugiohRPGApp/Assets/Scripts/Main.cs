using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Created by: Matthew Sahakian
Oct. 10 2023
Overview: Controls unity imput behavior and manages the transitions between windows.
--------------------------------------------------------------------------------------------------------
*/
public class Main : MonoBehaviour
{
    public List<Character> Characters = new List<Character>(); //this list needs to persist between plays. Itll be saved any time a player moves to edit a character within the list
    
    public PlayerDataModel playerData; //this is the datamodel used for saveing our list of characters.
    
    public GameObject window;
    public GameFSM stateMachine;
    public GameObject backBtn;

    private string introMessageP = "Feel free to create a new character with the icon to the lower left. Once you've made a character the icon on the lower right can be selected to view your created characters.";
    private string introMessageH1 = "Welcome to the Yu-gi-oh RPG!";

    public GameObject popUp;
    private void Awake()
    {
        playerData = new PlayerDataModel();
        stateMachine = GetComponent<GameFSM>();
       
    }

    private void Start()
    {
        StartCoroutine("LoadMethod");
        SaveLoadManager.SaveData(playerData);
    }

    
    //Public methods for interface
    public IEnumerator LoadMethod() 
    {
        Characters.Clear();
        
        playerData = SaveLoadManager.LoadData();
        if (playerData != null)
        {
            //playerData = new PlayerDataModel();
            foreach (var character in playerData.charList)
            {
                Characters.Add(character);
            }
        }
        else 
        {
            popUp.GetComponent<PopUpUI>().OpenWindowSetupFields(introMessageH1, introMessageP);
        }

        yield return new WaitForSeconds(0.1f);
    }

    public void charCreatorBtn()
    { 
        StartCoroutine(CleanUpAndExit(3));
    }

    public void ViewCharacterButton() 
    {
        StartCoroutine(CleanUpAndExit(2));
    }

    public void BackButtons() 
    {
        StartCoroutine(CleanUpAndExit(-1));
    }

    private void OnEnable()
    {
        window.SetActive(true);
        backBtn.SetActive(false);
    }

    private void OnDisable()
    {
        if (Application.isPlaying) 
        {
            backBtn.SetActive(true);
            window.SetActive(false);
        }
       
        //playerData.CharacterListSaveData(Characters);
    }

    public IEnumerator CleanUpAndExit(int i) 
    {
        stateMachine.ChangeState(i);
        yield return new WaitForSeconds(0.5f);
    }
}
