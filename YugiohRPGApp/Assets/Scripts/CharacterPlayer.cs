using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
/// <summary>
/// This class opens the play window. In this window; players may see their stats, roll skill checks, make 
/// adjustments to their Hitpoints, Cards per day, and select from their availiable abilities.
/// </summary>
public class CharacterPlayer : GameScreen
{
    string _ath;
    string _cha;
    string _int;
    string _characterDesignation;
    string _cpd;
    int cpdVal;
    int playCpdVal;
    int hpVal;
    int playHPVal;
    string _hitpoints;

    public TMP_Text SSlv;

    public TMP_Text A_Rank;
    public TMP_Text C_Rank;
    public TMP_Text I_Rank;

    public TMP_Text charName;

    public TMP_Text Hitpoints;
    public TMP_Text curHitpoints;
    public TMP_Text CPD;
    public TMP_Text curCPD;
    public TMP_Text armorVal;

    public GameObject popUpWindow;
    public GameObject _CardUI;
    private PopUpUI popUIScript;
    // Start is called before the first frame update
    void Start()
    {
        
        popUIScript = popUpWindow.GetComponent<PopUpUI>();
    }

    private void Awake()
    {
        gameFSM = GetComponent<GameFSM>();
        main = GetComponent<Main>();
        
    }
    private void OnEnable()
    {
        
        window.SetActive(true);
        exitBtn.onClick.AddListener(BackButtonPress);
        main.enabled = false;
        HideAbilityButtons();
        SaveLoadManager.SaveData(main.playerData); //Saves all data when entering playmode.
        Initialize();
    }

    public override void Initialize()
    {
        _characterDesignation = main.Characters[lookUpID].ReadCharacterName() + " : " + main.Characters[lookUpID].myClasses[0].className;
       
        _ath = main.Characters[lookUpID].AthRank;
        _cha = main.Characters[lookUpID].ChaRank;
        _int = main.Characters[lookUpID].IntRank;

        cpdVal = main.Characters[lookUpID].CPD;
        playCpdVal = cpdVal;
        curCPD.text = cpdVal.ToString();
        hpVal = main.Characters[lookUpID].ReturnHitpoints();
        playHPVal = hpVal;
        curHitpoints.text = playHPVal.ToString();

        _cpd = "/ " + main.Characters[lookUpID].CPD.ToString();
        _hitpoints = "/ " + main.Characters[lookUpID].ReturnHitpoints().ToString();
        //TextManager.Instance.QueMessage("Playing game as: " + main.Characters[lookUpID].ReadCharacterName()); 

        charName.text = _characterDesignation;
        A_Rank.text = _ath;
        C_Rank.text = _cha;
        I_Rank.text = _int;

        CPD.text = _cpd;
        Hitpoints.text = _hitpoints;
        SetUpAbilityList();
        DisplaySSLV();
    }

    private void Update()
    {
        if (playCpdVal <= 0)
        {
            _CardUI.GetComponent<Button>().interactable = false;
        }
        else 
        {
            _CardUI.GetComponent<Button>().interactable = true;
        }

    }

    public void DisplaySSLV() 
    {
        SSlv.text = "SS LV. " + main.Characters[lookUpID].SSLevel.ToString();
    }

    #region skillchecks
    public void RollSkillCHA()
    {
        int result = main.Characters[lookUpID].RollDiceByRank(2, C_Rank.text);
        string contentResult = _characterDesignation + ". has rolled a result of : " + result.ToString() + ".";
        popUIScript.OpenWindowSetupFields("Charisma Roll", contentResult);
    }
    public void RollSkillINT()
    {
        int result = main.Characters[lookUpID].RollDiceByRank(2, I_Rank.text);
        string contentResult = _characterDesignation + ". has rolled a result of : " + result.ToString() + ".";
        popUpWindow.GetComponent<PopUpUI>().OpenWindowSetupFields("Intellect Roll", contentResult);
    }
    public void RollSkillATH() 
    {
        int result = main.Characters[lookUpID].RollDiceByRank(2, A_Rank.text);
        string contentResult = _characterDesignation + ". has rolled a result of : " + result.ToString() + ".";
        popUpWindow.GetComponent<PopUpUI>().OpenWindowSetupFields("Atheletics Roll", contentResult);
    }
    #endregion 

    #region Player Modified values
    //Rest will be used by a button to reset values in play mode.
    public void Rest() 
    {
        //set the cpd and health back to their initial play values.
        playHPVal = hpVal;
        curHitpoints.text = playHPVal.ToString();
        playCpdVal = cpdVal;
        curCPD.text = playCpdVal.ToString();
    }
    public void UseCard() 
    {
        if (playCpdVal <= 0) 
        {
            curCPD.text = "0";
            return;
        }
        playCpdVal--;
        curCPD.text = playCpdVal.ToString();
    }
    public void ReduceHP(string upVal)
    {
        int x;
        int.TryParse(upVal, out x);
        if (x + playHPVal > hpVal) //if the value to add plus your current hitpoints is greater than the max
                                  //set the current value to the max value
        {
            playHPVal = hpVal;
            curHitpoints.text = playHPVal.ToString();
            return;
        }
        if (playHPVal + x < 0) //if the playerHP value subtracted by the down value 
                              //is going to make the value result in a number below 0
        {
            playHPVal = 0;
        }
        else 
        {
            playHPVal += x; 
        }
        
        curHitpoints.text = playHPVal.ToString();
    }

    public void ArmorValChange(string upVal) 
    {
        int x;
        int.TryParse(upVal, out x);

        armorVal.text = x.ToString();
    }
    #endregion

    #region Ability Display

    //we have a bunch of buttons which can be placeholders... we only want to enable the buttons we need. We have a list of buttons an index and the button object. 
    public List<GameObject> _abilityButtonList;
    //method
    void SetUpAbilityList() 
    {
        List<ClassAbilityObj> casheList = main.Characters[lookUpID].myClasses[0].avaliableAbilityObjects;

        for (int i = 0; i < casheList.Count; i++) //This for loop is going to generate one button per ability object.
        {
            _abilityButtonList[i].gameObject.SetActive(true);
            _abilityButtonList[i].GetComponentInChildren<AbilityInfoStorage>().SetAbilityInformation(casheList[i]._featureName, casheList[i]._featureDescription);
        }
    }

    void HideAbilityButtons() 
    {
        foreach (GameObject b in _abilityButtonList)
        {
            b.gameObject.SetActive(false);
        }
    }

    #endregion

}
