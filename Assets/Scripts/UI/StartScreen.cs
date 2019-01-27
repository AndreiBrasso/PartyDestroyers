using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartScreen : MonoBehaviour {

    public InputField twitchChannel;

    public event Action<string> OnTwitchChannelSelected;

    public void OnTwitchChannelChange()
    {
        if (OnTwitchChannelSelected != null)
        {
            OnTwitchChannelSelected(twitchChannel.text);
        }
    }

    public void OnStartGame()
    {
        Debug.Log("start");
        GameManager.self.Restart();
        this.gameObject.SetActive(false);
    }
}
