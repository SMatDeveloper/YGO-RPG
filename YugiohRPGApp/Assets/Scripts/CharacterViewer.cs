using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
 * Created by matthew sahakian
 * Date oct. 12
 * 
 */
public class CharacterViewer : GameScreen
{
    
    //public TMP_Text[] textContainers; //I would like to remove this or replace it with a list.
    #region CharacterInfoBasic
    public TMP_Text charName;
    public TMP_Text charLevel;
    public TMP_Text totalEXP;//This will display all of the EXP earned at the current time for the character.
    public TMP_InputField EXP;
    #endregion

    #region ClassModifiedInformation
    public TMP_Text totalAbv;
    public TMP_Text remainingAbv;
    public TMP_Text Hitpoints;
    public TMP_Text CPD;
    public TMP_Text SSLV;
    #endregion

    #region CoreStats
    public TMP_Text athRank;
    public TMP_Text chaRank;
    public TMP_Text intRank;

    public TMP_Text athScore;
    public TMP_Text chaScore;
    public TMP_Text intScore;

    public TMP_InputField _ATH;
    public TMP_InputField _CHA;
    public TMP_InputField _INT;
    #endregion


    #region ButtonsAndPageAccessories
    public Button AcceptBtn;
    public Button levelUpBtn; //This button will only be displayed when the character has remaining levels to invest.
    public GameObject popup;
    #endregion


    //These will be moved to display on the play page
    /*
    public GameObject AbilityUIInvintory;
    public GameObject AbilityUIButton;

    public List<GameObject> abilityObjects = new List<GameObject>();
    */

    //---------------------------FUNCTIONS------------------------------------
    private void OnEnable()
    {
        Initialize();
        exitBtn.onClick.AddListener(BackButtonPress);
        main.enabled = false;
    }
    private void OnDisable()
    {
       // ClearAbilitiesAll();
    }

    private void Update() // This is a temporary area while i figure out how i wish to handle leveling up classes in the editor.
    {
        if (main.Characters[GameScreen.lookUpID].ReadRemainingAbP() > 0)
        {
            AcceptBtn.gameObject.SetActive(false);
            levelUpBtn.enabled = false;
        }
        else 
        {
            AcceptBtn.gameObject.SetActive(true);
            levelUpBtn.enabled = true;
        }
    }

    //Display abilities availiable to the character.
    /*public void DisplayAbilitysAll() 
    {
        foreach (ClassAbilityObj ability in main.Characters[lookUpID].myClasses[0].avaliableAbilityObjects) 
        {
            GameObject newObj = Instantiate(AbilityUIButton, AbilityUIInvintory.transform);
            newObj.GetComponent<AbilityItemUI>().GenerateItemValues(ability._featureName, ability._featureDescription, popup);

            abilityObjects.Add(newObj);
        }//need to create a cleanup function to destroy all of the objects between loading...
    }
    public void ClearAbilitiesAll() 
    {
        foreach (GameObject gO in abilityObjects) 
        {
            Destroy(gO);
        }
    }

    */

    public override void Initialize() //This is the gamescreen ovverride function.
    {
        gameFSM = GetComponent<GameFSM>();
        main = GetComponent<Main>();
        
        window.SetActive(true);
        readCharacterDataToTextContainers(GameScreen.lookUpID);
        /* THIS SECTION IS USED FOR TESTING AND FEEDBACK--------------------------------------------------------------------------------------------------
         * TextManager.Instance.QueMessage("Character Class: " + main.Characters[GameScreen.lookUpID].myClasses[0].className + " CPD modifier: "
            + main.Characters[GameScreen.lookUpID].myClasses[0].CPDMod + " Spirit Summon modifier: "
            + main.Characters[GameScreen.lookUpID].myClasses[0].SSLevel + " Ability Bonus: " + main.Characters[GameScreen.lookUpID].myClasses[0].AbVBonus);
        */
        //THIS WOULD DISPLAY THE ABILITIES ON THIS PAGE USING ABILITY NODES
        //DisplayAbilitysAll();
    }

    public void readCharacterDataToTextContainers(int ID)
    {
        //lookUpID = ID; Redundant
        totalEXP.text = "Total EXP: " + main.Characters[ID].ReadExp().ToString();
        charName.text = main.Characters[ID].ReadCharacterName();
        charLevel.text = main.Characters[ID].IncreaseEXP(0).ToString();
        Hitpoints.text = main.Characters[ID].ReturnHitpoints().ToString();
        athScore.text = main.Characters[ID].ReturnAthVal().ToString();
        chaScore.text = main.Characters[ID].ReturnChaVal().ToString();
        intScore.text = main.Characters[ID].ReturnIntVal().ToString();

        athRank.text = main.Characters[ID].AthRank;
        chaRank.text = main.Characters[ID].ChaRank;
        intRank.text = main.Characters[ID].IntRank;

        CPD.text = main.Characters[ID].CPD.ToString();
        SSLV.text = main.Characters[ID].myClasses[0].SSLevel.ToString();

        totalAbv.text = main.Characters[ID].ReadAbP().ToString();
        remainingAbv.text = main.Characters[ID].ReadRemainingAbP().ToString();

        if (main.Characters[ID].remainingLevelsToInvest > 0)
        {
            levelUpBtn.gameObject.SetActive(true);
        }
        else 
        {
            levelUpBtn.gameObject.SetActive(false);
        }
        
    }
    #region CoreStats Updaters
    public void UpdateATHStats(string val)
    {
        if (val == "")
        { return; }
        int resultVal = int.Parse(val);
        main.Characters[lookUpID].IncreaseAbilityScore(resultVal, 1);
        readCharacterDataToTextContainers(lookUpID);
        _ATH.text = "";
    }
    public void UpdateCHAStats(string val)
    {
        if (val == "")
        { return; }
        int resultVal = int.Parse(val);

        main.Characters[lookUpID].IncreaseAbilityScore(resultVal, 2);
        readCharacterDataToTextContainers(lookUpID);
        _CHA.text = "";
    }
    public void UpdateINTStats(string val)
    {
        if (val == "")
        { return; }
        int resultVal = int.Parse(val);

        main.Characters[lookUpID].IncreaseAbilityScore(resultVal, 3);
        readCharacterDataToTextContainers(lookUpID);
        _INT.text = "";
    }
    #endregion

    #region LevelUp Updaters
    public void onEXPChange(string val) //Reads input from the EXP entry field and sends the result out to the Main.Characters[].updateEXP.
    {
        if (val == "") //empty results are null
        {
            EXP.text = main.Characters[GameScreen.lookUpID].ReadExp().ToString();
            return;
        }
        int result = int.Parse(val);
        if (result < 0)  //wont accept negitave values.
        {
            EXP.text = main.Characters[GameScreen.lookUpID].ReadExp().ToString();
            return;
        }
        EXP.text = main.Characters[GameScreen.lookUpID].ReadExp().ToString();

        main.Characters[GameScreen.lookUpID].IncreaseEXP(result); //excexutes increase
        readCharacterDataToTextContainers(GameScreen.lookUpID);

    }
    //This is going to be responsible for increasing a level in the character and drawing forth its new updated stats.
    public void InvestLevelButtons() 
    {
        main.Characters[lookUpID].InvestALevel();
        readCharacterDataToTextContainers(lookUpID);
    }

    #endregion
    public void onGenerateByClass() //This function is relational to a button which "Levels Up" a character.
    {
        main.Characters[GameScreen.lookUpID].EvaluateCPD();
        main.Characters[GameScreen.lookUpID].EvaluateHitpoints();
        readCharacterDataToTextContainers(GameScreen.lookUpID);
    }

    public void onFinishedEdit() 
    {
        StartCoroutine(CleanUpExit(5));
    }

}
