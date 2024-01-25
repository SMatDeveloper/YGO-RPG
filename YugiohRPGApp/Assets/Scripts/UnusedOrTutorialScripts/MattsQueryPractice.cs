using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleSQL;
public class DoAQuery : MonoBehaviour
{
    public TMP_Text output;
    public SimpleSQLManager dbManager;
    // Start is called before the first frame update
    void Start()
    {
        var results = dbManager.Query<SimpleSQL.Demos.GameClass>("SELECT * FROM Classes"); //query outputs a list to be used. You must have defined a data container such as the gameclass type prior to use.

        output.text = "";
        foreach (var result in results)
        {
            output.text += result.Name + "\n";
        }
    }
}

