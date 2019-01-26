using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
[RequireComponent(typeof(TwitchResponses))]
public class TwitchEventTimer : MonoBehaviour {

    // Use this for initialization

    private float time;
    private bool timeIsTicking = false;
    public float eventTime = 20.0f;

    public Dictionary<string, List<string>> votes;

    private TwitchResponses TR;
    private TwitchIRC IRC;

    void OnChatMsgRecieved(string msg)
    {
        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        //add new message.
        TR.HandleMessage(user, msgString);
    }

    void StartNewEvent(GameEvent gameEvent)
    {
        if (timeIsTicking == true) {
            EventStop();
        }
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

    }

	void Start ()
    {
        IRC = this.GetComponent<TwitchIRC>();
        TR = this.GetComponent<TwitchResponses>();

        IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
        
        GameManager.self.eventManager.OnEventChanged += StartNewEvent;
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
