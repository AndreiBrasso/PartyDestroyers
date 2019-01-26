using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent
{
    public string EventID;
    public string EventRequierments;
    public int Time_Min;
    public int Time_Max;
    public string Text;
    public string EventEffect;
    public string Option1;
    public string Option1Effect;
    public string Option2;
    public string Option2Effect;
    public bool NotFinished;

    public Dictionary<ParameterType, int> evtEffects;
    public Dictionary<ParameterType, int> o1Effect;
    public Dictionary<ParameterType, int> o2Effect;

    public void ParseEffects()
    {
        evtEffects = GetParameters(EventEffect);
        o1Effect = GetParameters(Option1Effect);
        o2Effect = GetParameters(Option2Effect);
    }

    private Dictionary<ParameterType, int> GetParameters(string encodedEffects)
    {
        var dictionary = new Dictionary<ParameterType, int>();
        var eEffects = EventEffect.Split(';');
        foreach (var e in eEffects)
        {

        }
        return dictionary;
    }
}

[Serializable]
public class EventsFromJson
{
    public GameEvent[] events;
}

[System.Serializable]
public class EffectParameter 
{
    public ParameterType parameterType;
    public Action<EffectParameter> OnFinished;
    public Action<EffectParameter> OnChanged;

    private float _currentValue;
    public float currentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            _currentValue = value;
            if(OnChanged != null)
            {
                OnChanged(this);
            }
            if (_currentValue == 0)
            {
                if(OnFinished != null)
                {
                    OnFinished(this);
                }
            }
            
        }
    }

    public void ResetParameter()
    {
        _currentValue = 0;
    }
}

public enum ParameterType
{
    People,
    House,
    Booze,
    Fun,
    Time,
    Money
}

