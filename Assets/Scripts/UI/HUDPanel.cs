using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDPanel : MonoBehaviour {

    [SerializeField]
    Text mainText;

    [SerializeField]
    Text option1Text;

    [SerializeField]
    Text option2Text;

    [SerializeField]
    Text option1VotesText;

    [SerializeField]
    Text option2VotesText;

    [SerializeField]

    Text twitchOptionTimeout;
    public float timeLeft = 20.0f;

    [SerializeField]
    Image[] values;

    float votes1 = 0.0f;
    float votes2 = 0.0f;

    private void Start()
    {
        GameManager.self.eventManager.OnEventChanged += NewGameEventSelected;
        GameManager.self.twitchResponses.OnChatVote += UpdateCrowdOptions;

        foreach (var p in GameManager.self.eventManager.parameters)
        {
            UpdateValue(p.Value);
        }
        SetListeners();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        twitchOptionTimeout.text = (timeLeft).ToString("0") + " sec";
        if (timeLeft < 0)
        {
            //Do something useful or Load a new game scene depending on your use-case
            twitchOptionTimeout.text = "Time to vote is out";

            if (votes1 > votes2)
            {
                SelectOption(1);
            }
            else if (votes1 < votes2)
            {
                SelectOption(2);
            }
            else
            {
                SelectOption(UnityEngine.Random.Range(1,2));
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.self.eventManager.OnEventChanged -= NewGameEventSelected;
        GameManager.self.twitchResponses.OnChatVote -= UpdateCrowdOptions;
        ClearListeners();
    }

    private void NewGameEventSelected(GameEvent newEvent)
    {
        mainText.text = newEvent.Text;
        option1Text.text = newEvent.Option1;
        option2Text.text = newEvent.Option2;

        timeLeft = 20.0f;
        option1VotesText.text = "";
        option2VotesText.text = "";

    }

    private void SetListeners()
    {
        for(int i = 0; i< values.Length; i++)
        {
            GameManager.self.eventManager.parameters[(ParameterType)i].OnChanged += UpdateValue;    
        }
    }

    private void ClearListeners()
    {
        for (int i = 0; i < values.Length; i++)
        {
            GameManager.self.eventManager.parameters[(ParameterType)i].OnChanged -= UpdateValue;
        }
    }

    public void UpdateValue(EffectParameter parameter)
    {
         
         values[(int)parameter.parameterType].fillAmount =  (parameter.currentValue / 100);
         
        
    }

    public void SelectOption(int option)
    {
        GameManager.self.eventManager.SelectOption(option);
        GameManager.self.PassTime();
    }

    public void UpdateCrowdOptions(Dictionary<string, List<string>> votes)
    {
        if (votes["Option1"]!=null)
        {
            votes1 = votes["Option1"].Count;
        }
        if (votes["Option2"] != null)
        {
            votes2 = votes["Option2"].Count;
        }
        float totalVotes = votes1 + votes2;

        option1VotesText.text = Math.Floor(votes1 * 100 / totalVotes).ToString() +"%";
        option2VotesText.text = Math.Floor(votes2 * 100 / totalVotes).ToString() +"%";
    }

}
