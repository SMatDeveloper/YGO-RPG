using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataModel
{
    public List<Character> charList = new List<Character>();
  
    //To Do: Create a structure for the saves inorder to break down and reformat the list into save and load methods.
    //we need to save out Character Class, and Character objects properly from a list and back into a list to overrite in the main...
}
