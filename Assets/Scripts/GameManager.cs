using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager self;

    public UIManager uiManager;
    public GraphicsAssets graphicsLibrary;
    public EventManager eventManager;
    public TwitchResponses twitchResponses;

    public float animationDelay
    {
        get
        {
            return (1.10f-(eventManager.parameters[ParameterType.Fun].currentValue / 100f))* 0.5f;
        }
    }

    private void Awake()
    {
        if(self == null)
        {
            self = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach(var parameter in eventManager.parameters)
        {
            parameter.Value.OnFinished += GameEnded;
        }
    }

    private void OnDestroy()
    {
        foreach (var parameter in eventManager.parameters)
        {
            parameter.Value.OnFinished -= GameEnded;
        }
    }

    public void GameEnded(EffectParameter parameter)
    {
        self.uiManager.retryPanel.gameObject.SetActive(true);

        switch (parameter.parameterType)
        {
            case ParameterType.Booze: self.uiManager.retryPanel.SetText("Party ended, at "+ self.eventManager.GetTimeReadable()+" you ran out of booze..."); break;
            case ParameterType.House: self.uiManager.retryPanel.SetText("Party ended, at " + self.eventManager.GetTimeReadable() + " your house is trashed!"); break;
            case ParameterType.Fun: self.uiManager.retryPanel.SetText("Party ended, at " + self.eventManager.GetTimeReadable() + " way too boring"); break;
            case ParameterType.Time: self.uiManager.retryPanel.SetText("Great Party! The last guests left at " + self.eventManager.GetTimeReadable() + "!"); break;
            case ParameterType.People: self.uiManager.retryPanel.SetText("Everyone left your party... Your party lasted until " + self.eventManager.GetTimeReadable()); break;
            case ParameterType.Money: self.uiManager.retryPanel.SetText("Everyone left your party... Your party lasted until " + self.eventManager.GetTimeReadable()); break;
        }        
    }

    public void Restart()
    {
        self.uiManager.retryPanel.gameObject.SetActive(false);
        self.eventManager.ResetParameters();
        self.eventManager.ResetHistory();
        self.eventManager.TriggerNextEvent();
    }

    public void PassTime()
    {
        self.eventManager.parameters[ParameterType.Time].currentValue++;
        self.eventManager.TriggerNextEvent();
    }
   
}
