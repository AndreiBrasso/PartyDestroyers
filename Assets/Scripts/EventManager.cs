using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManager : MonoBehaviour
{

    #region Fields 
    public TextAsset textAsset;
    public GameEvent[] gameEvents;

    [SerializeField]
    EffectParameter[] effectParameters;
    public Dictionary<ParameterType, EffectParameter> parameters;

    private List<string> optionHistory;
    private List<string> eventHistory;
    private GameEvent currentEvent;

    public event Action<GameEvent> OnEventChanged;

    public string GetTimeReadable()
    {
        float timePassed = parameters[ParameterType.Time].currentValue;

        float gameMinutes = timePassed / 50f * 8 * 60;

        int hoursPlayed = Mathf.FloorToInt(gameMinutes / 60);
        int minutesLeft = Mathf.FloorToInt(gameMinutes) - hoursPlayed * 60;

        if (hoursPlayed > 4)
        {
            return "0" + (hoursPlayed - 4).ToString() + ":" + (minutesLeft < 10 ? "0" : "") + minutesLeft.ToString();
        }
        else
        {
            return "2" + hoursPlayed.ToString() + ":" + (minutesLeft < 10 ? "0" : "") + minutesLeft.ToString();
        }

    }

    public void ResetParameters()
    {
        foreach (var p in effectParameters)
        {
            p.ResetParameter();
        }
    }

    public void TriggerNextEvent()
    {
        var finishedEvents = FilterOutNotFinished();
        var eventsAtThisTime = GetEventsAtThisTime(finishedEvents);
        var eventsThatFit = ChooseEventsBasedOnParameters(eventsAtThisTime);
        var eventsUnused = FilterUsedOnes(eventsThatFit);
        if (eventsUnused.Length == 0)
        {
            Debug.LogWarning("No more usable events");
            currentEvent = null;
            GameManager.self.GameEnded(parameters[ParameterType.Time]);
        }
        else
        {
            currentEvent = ChooseRandomEvent(eventsUnused);
        }

        if (currentEvent == null)
        {
            Debug.LogWarning("No event found");
        }
        else
        {
            Debug.Log(currentEvent.Text);
            if (eventHistory == null) eventHistory = new List<string>();
            eventHistory.Add(currentEvent.EventID);
            foreach (var effect in currentEvent.evtEffects)
            {
                parameters[effect.Key].currentValue += effect.Value;
            }
            if (OnEventChanged != null)
            {
                OnEventChanged(this.currentEvent);
            }
        }
    }

    public void SelectOption(int option)
    {
        if (currentEvent != null)
        {
            string selectedOption = "E" + currentEvent.EventID + "_" + option;
            if (optionHistory == null)
            {
                optionHistory = new List<string>();
            }
            optionHistory.Add(selectedOption);

            if (option == 1)
            {
                foreach (var effect in currentEvent.o1Effect)
                {
                    parameters[effect.Key].currentValue += effect.Value;
                }
            }
            else
            {
                foreach (var effect in currentEvent.o2Effect)
                {
                    parameters[effect.Key].currentValue += effect.Value;
                }
            } 
        }
    }

    public void ResetHistory()
    {
        optionHistory = new List<string>();
        eventHistory = new List<string>();
        currentEvent = null;
    }

    private void Awake()
    {
        parameters = new Dictionary<ParameterType, EffectParameter>();
        foreach (var e in effectParameters)
        {
            if (!parameters.ContainsKey(e.parameterType)) parameters.Add(e.parameterType, e);
        }
        string jsonContent = textAsset.text;
        var eventsFromJson = JsonUtility.FromJson<EventsFromJson>(jsonContent);
        this.gameEvents = eventsFromJson.events;
        foreach (var e in gameEvents)
        {
            e.ParseEffects();
            e.ParseRequirements();
        }
    }

    private GameEvent[] FilterOutNotFinished()
    {
        return (from evt in gameEvents
                where !evt.NotFinished
                select evt).ToArray<GameEvent>();
    }

    private GameEvent[] GetEventsAtThisTime(GameEvent[] gameEvents)
    {
        float currentTime = parameters[ParameterType.Time].currentValue;
        Debug.Log("Time:" + currentTime);

        return (from evt in gameEvents
                where currentTime < evt.Time_Max && currentTime >= evt.Time_Min
                select evt).ToArray<GameEvent>();
    }

    private GameEvent ChooseRandomEvent(GameEvent[] events)
    {
        int rIndex = UnityEngine.Random.Range(0, events.Length);
        return (rIndex < events.Length) ? events[rIndex] : null;
    }

    private GameEvent[] FilterUsedOnes(GameEvent[] events)
    {
        List<GameEvent> notUsed = new List<GameEvent>();
        for (int i = 0; i < events.Length; i++)
        {
            if (!eventHistory.Contains(events[i].EventID))
            {
                notUsed.Add(events[i]);
            }
        }
        return notUsed.ToArray();
    }

    private GameEvent[] ChooseEventsBasedOnPrevious(GameEvent[] events)
    {
        List<GameEvent> eventsThatFit = new List<GameEvent>();
        foreach (var e in events)
        {
            if (e.EventRequierments == "")
            {
                eventsThatFit.Add(e);
                continue;
            }
            var s = optionHistory.Find(x => x == e.EventRequierments);
            if (!string.IsNullOrEmpty(s))
            {
                eventsThatFit.Add(e);
                continue;
            }
        }
        return eventsThatFit.ToArray();
    }

    private GameEvent[] ChooseEventsBasedOnParameters(GameEvent[] events)
    {
        List<GameEvent> eventsThatFit = new List<GameEvent>();
        foreach (var e in events)
        {
            if (e.parameterRequirements == null || e.parameterRequirements.Count == 0)
            {
                eventsThatFit.Add(e);
            }
            else
            {
                bool fits = true;

                foreach (var req in e.parameterRequirements)
                {
                    if (req.Value > 0 && parameters[req.Key].currentValue < req.Value)
                    {

                        fits = false;
                        break;
                    }
                    if (req.Value < 0 && parameters[req.Key].currentValue > Mathf.Abs(req.Value))
                    {
                        fits = false;
                        break;
                    }
                }
                if (fits)
                {
                    eventsThatFit.Add(e);
                }
            }
        }
        return eventsThatFit.ToArray();
    }
    #endregion
}
