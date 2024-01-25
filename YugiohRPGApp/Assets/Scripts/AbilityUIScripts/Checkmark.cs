using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script is made to change the checkmark ui to its selected or unselected states.
public class Checkmark : MonoBehaviour
{
    public Button checkmark;
    public Button abilityButton;
    public Sprite _check;
    public Sprite _uncheck;
    bool _isChecked;

    public void Start()
    {
        _isChecked = false;
        abilityButton.interactable = _isChecked;
    }
    public void CheckedAbility() 
    {
        switch (_isChecked)
        {
            case true: 
                checkmark.image.sprite = _uncheck;
                _isChecked = false;
                break;
            case false:
                checkmark.image.sprite = _check;
                _isChecked = true;
                break;
        }

        abilityButton.interactable = _isChecked;
        //when clicked show the next sprite in the array.
    }

    private void OnEnable()
    {
        _isChecked = false;
        abilityButton.interactable = _isChecked;
    }
}
