using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
[RequireComponent(typeof(TwitchResponses))]
public class EventTimer : MonoBehaviour {

    // Use this for initialization

    private float time;
    private bool timeIsTicking = false;
    public float eventTime = 30.0f;

    public Dictionary<string, List<string>> votes;

    private TwitchResponses TR;
    private TwitchIRC IRC;

    void StartNewEvent()
    {
        IRC.SendMsg("A new event is starting soon, vote now with !vote and number of your option");

        time = eventTime;
        timeIsTicking = true;
        
        TR.InitVotes();
        TR.openEvent = true;
        
        Debug.Log("StartEvent");
    }

    void EventStop()
    {
        timeIsTicking = false;

        TR.openEvent = false;

        IRC.SendMsg("Event vote ended. Option1 - "+ TR.votes["Option1"].Count + " votes Option2 - "+ TR.votes["Option2"].Count+" votes");
        Debug.Log("EndEvent");

        StartNewEvent();
    }

	void Start ()
    {
        IRC = this.GetComponent<TwitchIRC>();
        TR = this.GetComponent<TwitchResponses>();

        StartNewEvent();
    }
	
	// Update is called once per frame
	void Update () {

        if (timeIsTicking) {
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                EventStop();
            }
        }
	}
}
