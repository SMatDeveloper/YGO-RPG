using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Static reference to the instance of the Singleton
    private static Singleton instance;

    // Public property to access the Singleton instance
    public static Singleton Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Ensure only one instance of the Singleton exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set the Singleton reference to this instance
        instance = this;

        // Optionally, prevent this object from being destroyed when loading new scenes
        DontDestroyOnLoad(this.gameObject);
    }
}
