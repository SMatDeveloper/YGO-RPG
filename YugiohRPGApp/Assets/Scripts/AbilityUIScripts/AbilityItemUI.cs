using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilityItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public string _abilityName = "Default";
    public string _abilityDescription;
    public TMP_Text _abilityText;
    public Image _abilityImage;
    public CharacterViewer cv;
    GameObject popUpWindow;
    //Popup on this.
    public void EnableandGeneratePopup() 
    {
       PopUpUI myPopup = popUpWindow.GetComponent<PopUpUI>();
        //myPopup.OpenWindowSetupFields(this);
    }
    public void GenerateItemValues(string name, string description, GameObject popupwindow) 
    {
        _abilityName = name;
        _abilityDescription = description;
        popUpWindow = popupwindow;
    }
    //------These were only relevent if the application is played on a computer. As the application will be a phone app these values are all irrelivant.
    public void OnPointerEnter(PointerEventData eventData) 
    {
        //enable the name text.
        _abilityText.text = _abilityName;
        //print("Ability Hover");
    }

    public void OnPointerClick(PointerEventData clickData) 
    {
        //generate a popup
        //populate the popup with the ability text and ability name.
        print("Ability Click: " + _abilityName);
        EnableandGeneratePopup();
        //TextManager.Instance.QueMessage(_abilityDescription); //Replace me with the popup window.
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        //disable the name text.
        _abilityText.text = " ";
        //print("Ability Hover Exit");
    }
    
}
