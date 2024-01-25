using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityInfoStorage : MonoBehaviour
{
    public string _name;
    public string _text;
    public PopUpUI _popup;
    public TMP_Text displayTXT;
    public void SetAbilityInformation(string name, string text) 
    {
        _name = name;
        _text = text;
        displayTXT.text = _name;
    }
    public void ReadAbilityInformation() 
    {
        _popup.OpenWindowSetupFields(_name, _text);
    }

}
