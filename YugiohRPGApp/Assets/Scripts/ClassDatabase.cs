using System.Collections;
using System.Collections.Generic;
using SimpleSQL;
public static class ClassDatabase
{
    public static Dictionary<int, SimpleSQL.Demos.ClassProgressionTable> _classProgressionTable = new Dictionary<int, SimpleSQL.Demos.ClassProgressionTable>();//Class progression tables to be accessed by class table ID.
    public static Dictionary<int, SimpleSQL.Demos.GameClass> _gameClass = new Dictionary<int, SimpleSQL.Demos.GameClass>();//storage dictionary for the classes.
    public static Dictionary<int, SimpleSQL.Demos.ClassAbility> _abilities = new Dictionary<int, SimpleSQL.Demos.ClassAbility>(); //all abilities are loaded into this list using their key and constructor datatype.

    public static void AddAbility(int abilityID, SimpleSQL.Demos.ClassAbility classAbility)
    {
        _abilities.Add(abilityID, classAbility);
    }
    public static void AddClass(int classID, SimpleSQL.Demos.GameClass gameClass)
    {
        _gameClass.Add(classID, gameClass);
    }
    public static void AddTable(int tableID, SimpleSQL.Demos.ClassProgressionTable classAbility)
    {
        _classProgressionTable.Add(tableID, classAbility);
    }
}
