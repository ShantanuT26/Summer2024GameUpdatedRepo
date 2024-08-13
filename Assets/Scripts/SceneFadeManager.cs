using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneFadeManager : MonoBehaviour
{
    private Color fadeColor;
    private float fadeOutSpeed = 1f;
    private float fadeInSpeed=1f;
    private bool isFadingOut;
    private bool isFadingIn;
    [SerializeField]private Image fadeImage;
    public static SceneFadeManager Instance;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        fadeColor.a = 0f;
        isFadingIn = false;
        isFadingOut = false;
        fadeImage.color = fadeColor;
    }
    public bool GetIsFadingOut()
    {
        return isFadingOut;
    }
    public void StartFadeOutCycle()
    {
        isFadingOut = true;
    }
    public void StartFadeInCycle()
    {
        isFadingIn = true;
    }
    private void Update()
    {
        if(isFadingOut)
        {
            Debug.Log("fadingout");
            if(fadeColor.a<1)
            {
                fadeColor.a += Time.deltaTime * fadeOutSpeed;
                fadeImage.color = fadeColor;
            }
            else
            {
                isFadingOut = false;
            }
        }
        else if(isFadingIn)
        {
            if(isFadingIn)
            {
                if (fadeColor.a>0)
                {
                    fadeColor.a -= Time.deltaTime * fadeInSpeed;
                    fadeImage.color = fadeColor;
                }
                else
                {
                    isFadingIn = false;
                }
            }
        }
    }
}
