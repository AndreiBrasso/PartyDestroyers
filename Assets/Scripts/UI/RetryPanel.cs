using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryPanel : MonoBehaviour
{

    [SerializeField]
    Text popupText;

    public void SetText(string text)
    {
        popupText.text = text;
    }

    public void OnRetry()
    {
        GameManager.self.Restart();
    }
    	 
}
