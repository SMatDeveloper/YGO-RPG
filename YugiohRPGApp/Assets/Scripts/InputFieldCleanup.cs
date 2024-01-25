using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputFieldCleanup : MonoBehaviour
{
    TMP_InputField thisInput;

    private void Awake()
    {
        thisInput = gameObject.GetComponent<TMP_InputField>();
    }
    public void ClearText() 
    {
        thisInput.text = "";
    }
}
