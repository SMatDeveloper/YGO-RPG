using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
//date 10.16.2023
//Creating an abstract for the rest of the game windows to inherit from.

public abstract class GameScreen : MonoBehaviour
{
    [HideInInspector]
    public Main main;
    [HideInInspector]
    public GameFSM gameFSM;
    public GameObject window;
    public static int lookUpID;
    public Button exitBtn;

    public abstract void Initialize();

    public void BackButtonPress() 
    {
        StartCoroutine(CleanUpExit(1));
    }
    public IEnumerator CleanUpExit(int val)
    {
        gameFSM.ChangeState(val);
        exitBtn.onClick.RemoveListener(BackButtonPress);
        window.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        if (val == 1)
        {
            main.enabled = true;
            lookUpID = 0;

        }
        print("My class ID is " +lookUpID);
        this.enabled = false;
    }
}
