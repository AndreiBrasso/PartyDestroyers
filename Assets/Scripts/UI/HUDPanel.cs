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
    Image[] values;
    [SerializeField]
    Image option1Filler;
    [SerializeField]
    Image option2Filler;

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
        Debug.Log("Update Options");
        float votes1 = 0.0f;
        float votes2 = 0.0f;
        if (votes["Option1"]!=null)
        {
            votes1 = votes["Option1"].Count;
        }
        if (votes["Option2"] != null)
        {
            votes2 = votes["Option2"].Count;
        }
        float totalVotes = votes1 + votes2;

        option1VotesText.text = Math.Floor(votes1 * 100 / totalVotes).ToString();
        option2VotesText.text = Math.Floor(votes2 * 100 / totalVotes).ToString();

        option1Filler.fillAmount = votes1 / totalVotes;
        option2Filler.fillAmount = votes2 / totalVotes;
    }

}
