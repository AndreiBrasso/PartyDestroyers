using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryPanel : MonoBehaviour
{
    public event Action<string> OnTwitchChannelSelected;

    [SerializeField]
    Text popupText;

    [SerializeField]
    InputField twitchChannel;

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

    public void OnTwitchChannelChange() {
        if (OnTwitchChannelSelected != null) {
            OnTwitchChannelSelected(twitchChannel.text);
        }
    }
    
    public void SetIcon(ParameterType pType)
    {
        this.icon.sprite = possibleIcons[(int)pType];
    }
}
