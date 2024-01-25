using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSQL;
/*
 * created by matthew sahakian
 * date: 10.21.2023
 * this class loads the database from memory and formats it into the c# class Database to be used in character creation.
 */
public class ClassDatabaseLoader : MonoBehaviour
{
    public SimpleSQLManager sQLManager;

    public void LoadClassDB() 
    {
        var classResults = sQLManager.Query<SimpleSQL.Demos.GameClass>("SELECT * FROM Classes");
        var tableResults = sQLManager.Query<SimpleSQL.Demos.ClassProgressionTable>("SELECT * FROM ProgressionTable");
        var abilityResults = sQLManager.Query<SimpleSQL.Demos.ClassAbility>("SELECT * FROM AbilitiesList");

        foreach (var result in classResults) 
        {
            ClassDatabase.AddClass(result.ClassID, result);
        }

        foreach (var result in tableResults) 
        {
            ClassDatabase.AddTable(result.TableID, result);
        }
        foreach(var result in abilityResults) 
        {
            ClassDatabase.AddAbility(result.AbilityID, result);
        }

    }
   
}
