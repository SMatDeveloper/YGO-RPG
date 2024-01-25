using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPDUI : MonoBehaviour
{
    public UIAnimationStyles myStyles;

    public void CardDraw() 
    {
        gameObject.SetActive(true);
        myStyles.TriggerUIAnimation();
    }
}
