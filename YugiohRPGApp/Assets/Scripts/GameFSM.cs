using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Created by: Matthew Sahakian
Oct. 10 2023
Overview: Controls the apps internal states and can trigger loading or events for scripts to respond to.
--------------------------------------------------------------------------------------------------------
 */

public class GameFSM : MonoBehaviour
{
    public enum IState {START, MAIN, SELECT, CHARACTERCREATE, VIEW, PLAY, ROLL, EXIT = -1};
    public IState CurrentState 
    { 
        get { return _CurrentState; } 
        set 
        {
            StopAllCoroutines();//this is used between calls to the ienumerator methods which play out the logic for this fsm. However i may elect to make this a game event trigger system and handle the logic elsewhere..
            _CurrentState = value;
            
            switch (CurrentState)
            {
                case IState.START:
                    print("----------Startup functions. Loading Characters. Loading information from database");
                    dbLoader.LoadClassDB();
                    CurrentState = IState.MAIN;
                    break;
                case IState.MAIN:
                    //do main loop stuff
                    print("----------Entering main loop and menu");
                    _main.enabled = true;
                    break;
                case IState.SELECT:
                    _characterSelector.enabled = true;
                    break;
                case IState.CHARACTERCREATE:
                    print("---------Entering Character creator");
                    _characterCreator.enabled = true;
                    break;
                case IState.VIEW:
                    _characterViewer.enabled = true;
                    break;
                case IState.ROLL:
                    break;
                case IState.PLAY:
                    print("--------Entering Tabletop Play mode");
                    _characterPlayer.enabled = true;
                    break;
                case IState.EXIT:
                    //UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                    break;
            }
        }
    }
    public ClassDatabaseLoader dbLoader;
    public CharacterCreator _characterCreator;
    public Main _main;
    public CharacterSelector _characterSelector;
    public CharacterViewer _characterViewer;
    public CharacterPlayer _characterPlayer;
    [SerializeField]
    IState _CurrentState = IState.START; //This will be the default state for the game.

    // Start is called before the first frame update
    void Awake()
    {
        _main = GetComponent<Main>();
        _characterCreator = GetComponent<CharacterCreator>();
        _characterSelector = GetComponent<CharacterSelector>();
        _characterViewer = GetComponent<CharacterViewer>();
        _characterPlayer = GetComponent<CharacterPlayer>();

        DiceManager.GenerateDiceList();
        CurrentState = IState.START;
    }

    //Retrieve current state as an int
    public int GetStateInt() 
    {
        return (int)CurrentState;
    }

    //Change the state using an int
    public void ChangeState(int newStateVal) 
    {
        CurrentState = (IState)newStateVal;
        print(CurrentState);
    }

}
