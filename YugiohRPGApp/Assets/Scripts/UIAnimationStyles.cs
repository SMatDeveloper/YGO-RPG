using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script is attatched to UI game objects which I wish to animate using lerp.
/// Created by Matthew Sahakian on 12/6/2023
/// </summary>
/// 
public class UIAnimationStyles : MonoBehaviour
{
    public enum AnimType
    {
        SCALE,FADE,SLIDE
    }

    public AnimType animType;
    private RectTransform uIRectElement;//grabbing the rect of the element this is attatched to.
    [HideInInspector]
    public Vector3 startScale;
    public Vector3 startPos;
    public Vector3 endPos = new Vector3(0, -50, 0);
    private Vector3 endScale = new Vector3(1,0,1);
    
    public float lengthTime =1.0f; // This will adjust the speed at which the animation completes.

    private void Awake()
    {
        uIRectElement = this.gameObject.GetComponent<RectTransform>(); //Forcing the element...
        startScale = uIRectElement.localScale; //saving its start scale
        startPos = uIRectElement.localPosition;
        //animType = AnimType.SCALE; //Incase something doesnt set this value...
    }

    public void TriggerUIAnimation()
    {
        switch (animType)
        {
            case AnimType.SCALE:
                //scale the thing
                StartCoroutine("ScaleFromCenter");
                break;
            case AnimType.FADE:
                //fade the thing
                break;
            case AnimType.SLIDE:
                //Slide the thing
                StartCoroutine("SlideDown");
                break;
            default:
                //scale the thing
                break;
        }
    }
    private void OnEnable()
    {
        uIRectElement.localScale = startScale;
    }
    private void OnDisable()
    {
        uIRectElement.localScale = startScale;
        uIRectElement.localPosition = startPos;
        StopAllCoroutines();
    }

    IEnumerator ScaleFromCenter() //This "Animation" will scale the 'y' axis to make it look like a tv turning off.
    {
        float curTime = 0.0f;
        while (curTime < lengthTime) 
        {
            curTime += Time.deltaTime;

            float t = curTime / lengthTime;
            uIRectElement.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null; 
        }
        gameObject.SetActive(false);
    }
    void FadeOut() { }//This will make the alpha of a image change so that it slowly becomes invisible.
    IEnumerator SlideDown() 
    {
        float curTime = 0.0f;
        while (curTime < lengthTime) 
        {
            curTime += Time.deltaTime;

            float t = curTime / lengthTime;
            uIRectElement.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    //This will make the object slide into view or retrun out of view.
}
