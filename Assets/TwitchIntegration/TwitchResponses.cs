using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchResponses : MonoBehaviour {

    public bool openEvent = false;
    public Dictionary<string, List<string>> votes;

    void Awake() {
        votes = new Dictionary<string, List<string>>();
    }

    public void HandleMessage(string user, string msgString)
    {
        Debug.Log("MSG:"+msgString);

        if (openEvent)
        {
            CheckPublicVote(user, msgString);
        }
    }

    public void InitVotes()
    {
        votes.Clear();
        votes.Add("Option1", new List<string> { });
        votes.Add("Option2", new List<string> { });
    }

    void CheckPublicVote(string user, string msgString)
    {
        Debug.Log("Check");
        switch (msgString)
        {
            case "!vote 1":
                if (votes["Option1"].IndexOf(user) == -1 && votes["Option2"].IndexOf(user) == -1)
                {
                    votes["Option1"].Add(user);
                } else
                {
                    Debug.Log("Already Voted");
                }
                
                break;

            case "!vote 2":
                if (votes["Option1"].IndexOf(user) == -1 && votes["Option2"].IndexOf(user) == -1)
                {
                    votes["Option2"].Add(user);
                }
                else
                {
                    Debug.Log("Already Voted");
                }

                break;

            default:
                return;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
