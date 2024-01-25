using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// This code was made to handle text based messages to the player however right now it is casuing a reccursive stack issue because its being called on loop
/// Created:11,7,2023
/// </summary>
public class TextManager : MonoBehaviour
{
    private static TextManager instance;

    public static TextManager Instance 
    {
        get { return instance; } 
    }
   
    public List<string> messageQue;//FIFO style que.
    public TMP_Text textDisplay;
    bool isWriting = false;
   
    public float typeSpeed = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set the Singleton reference to this instance
        instance = this;

        // Optionally, prevent this object from being destroyed when loading new scenes
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!isWriting && messageQue.Count > 0)
        {
            StartCoroutine(DequeMessage());
        }
    }

    public IEnumerator DequeMessage()
    {
        isWriting = true;
        if (messageQue.Count > 0)
        {
            string message = messageQue[0];
            yield return StartCoroutine(WriteText(message));
            messageQue.RemoveAt(0);
        }
        else
        {
            print("Que is Empty...");
        }
        isWriting = false;
    }

    public void QueMessage(string val) 
    {
        messageQue.Add(val);
    }

    public IEnumerator WriteText(string message) 
    {
        textDisplay.text = "";
        if (message.Length <= 0)
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            foreach (char c in message)
            {
                textDisplay.text += c;
                yield return new WaitForSeconds(typeSpeed);
            }
            isWriting = false;
        }
    }
    public void DisplayText(string message) 
    {
        //To do: make method to display full message without typing/writing effect.
    }
}
