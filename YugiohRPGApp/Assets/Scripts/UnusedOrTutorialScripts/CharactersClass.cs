using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleSQL;
/*
 * Created by matthew sahakian
 * This becomes an object which data can be read to inorder to store the characters class' details.
 */
[System.Serializable]
public class CharactersClass
{
    public int classID;
    public string className;
    public int classDice; //int to represent the 3 abilities. 1 is Athletics, 2 is Charisma, 3 is Intelect.
    public int classLevel;
    public int hitpointMod;

    public int myTableKey;
    public int SSLevel;
    public int AbVBonus;
    public int CPDMod;
    public int abilityKey;
    public int lastAbilityKey;

    public List<ClassAbilityObj> avaliableAbilityObjects = new List<ClassAbilityObj>(); //This list will be updated overtime with additions happening on level up.

    public CharactersClass(int i)
    {
        GenerateNewClassByKey(i);
        StoreTableAtClassLevel(1);// this needs to be accessable to level up the character at runtime.
    }

    //method to ovverride values to new level
    public void StoreTableAtClassLevel (int newLevel)
    {
        //create a new dictionary of table entries only including the entries which contain the Class ID of this class.
        //from this new entry of tables grab the one which has the correct level entered above.
        lastAbilityKey = abilityKey;
        var results = ClassDatabase._classProgressionTable.Where(x => x.Value.ClassID == classID && x.Value.Level == newLevel)
                                   .ToDictionary(x => x.Key, x => x.Value);
        //this filtered by class ID should return a dictionary only containing the entries which are of THIS classes id.

        foreach (var result in results) //search through the dictionary which was created and stored at the current level and override this classes current stats with the new dictionary results.
        {
            SSLevel = result.Value.SSLevel;
            AbVBonus = result.Value.AbVBonus;
            CPDMod = result.Value.CPDMod;
            myTableKey = result.Key;
            abilityKey = result.Value.AdditionalAbilities;
        }
        //when storing the ability values at _new level we will grab the ability ID from the table and call the AddAbility() method to populate the list of ability objects.
        if(abilityKey == lastAbilityKey) 
        {
            Debug.Log("Ability Values to be added were the same as previous levelup");
            return; }//checks the value of the ability key before and after the look up. if the value is the same it should not populate the abilities with anything lower.
        AddClassFeatures(abilityKey);
    }

    //ToDo: Generate a list of class ability objects which are added to this character class.
    public void AddClassFeatures(int aK) //this will be passed the ability id of the highest attained ability at the current level.
    {
        //If there is no new features at this level immediately break out and dont populate the list.
        //Search through the database and return all entries which belong to this class id and have an ability id lower than the passed key.
        var results = ClassDatabase._abilities.Where(x => x.Value.ClassID == classID && x.Key <= aK && x.Key > lastAbilityKey).ToDictionary(x => x.Key, x => x.Value);
        foreach(var result in results) 
        {
            ClassAbilityObj objToAdd = new ClassAbilityObj(result.Value.AbilityID, result.Value.AbilityName, result.Value.AbilityText);
            avaliableAbilityObjects.Add(objToAdd);
            Debug.Log(objToAdd._featureName + " " + objToAdd._featureID);
        }
        //use the returned dictionary to create new class ability object(s) and save them to the list.
        //Check for duplicates and dont repopulate those ones?
    }
    

    public void GenerateNewClassByKey(int ID) 
    {
        SimpleSQL.Demos.GameClass myClass = ClassDatabase._gameClass[ID];
        classID = myClass.ClassID;
        className = myClass.Name;
        classDice = myClass.ClassDice;
        hitpointMod = myClass.HitpointMod;

        classLevel = 1;   
    }



}
