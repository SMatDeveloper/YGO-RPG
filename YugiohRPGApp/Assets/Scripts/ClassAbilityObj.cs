using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is responsible for the abilities which should be loaded and saved onto the characters based on their level.
/// perhaps this should be a scriptable object...
/// </summary>
[System.Serializable]
public class ClassAbilityObj
{
    //Ability ID
    public int _featureID;
    //Ability Name
    public string _featureName;
    //Ability Description
    public string _featureDescription;

    public ClassAbilityObj(int id, string name, string feature) 
    {
        _featureID = id;
        _featureName = name;
        _featureDescription = feature;
    }
}
