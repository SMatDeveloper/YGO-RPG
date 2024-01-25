using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopUpUI : MonoBehaviour
{
    public TMP_Text popupTitle;
    public TMP_Text popupContent;
    public GameObject parent;
    public UIAnimationStyles myAnim;
    public void OpenWindowSetupFields(string title, string content) 
    {
        gameObject.SetActive(true);
        gameObject.transform.position = parent.transform.position;
        popupContent.text = content;
        popupTitle.text = title;
    }

    public void CloseWindow() 
    {
        myAnim.TriggerUIAnimation();
        //gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        popupContent.text = "";
        popupTitle.text = "";
    }
}
