using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryPanel : MonoBehaviour
{
   

    [SerializeField]
    Text popupText;

    public Text time;
    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite[] possibleIcons;

   

    public void SetText(string text)
    {
        popupText.text = text;
    }

    public void OnRetry()
    {
        GameManager.self.Restart();
    }

    
    
    public void SetIcon(ParameterType pType)
    {
        
        this.icon.sprite = possibleIcons[(int)pType];
    }
    public void SetTime(string text)
    {
        this.time.text = text;
    }
}
