using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * Created by Matthew Sahakian
 * Date Nov. 3 2023
 * This class will be responsible for loading and saving data while using the application. 
 * It will use the Json format to save out a list of the characters. This list will most likely be taken from the MAIN script.
*/

public class SaveLoadManager : MonoBehaviour
{
    //To do: Create a persistent save path. Path must work for mobile devices. I remember reading that the file ending needed to be accurate for mobile IOS.

    public static void SaveData(PlayerDataModel saveModel) 
    {
        //grab the model for data storage.
        string json = JsonUtility.ToJson(saveModel);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public static PlayerDataModel LoadData() 
    {
        string path = Application.persistentDataPath + "/save.json";

        if (File.Exists(path)) 
        {
            //firstRun = false;
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerDataModel>(json);
        }
        else 
        {
            print("File did not Exist.");
            //return new PlayerDataModel();
            return null;
        }
    }
    
}
