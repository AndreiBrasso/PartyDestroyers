using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    [SerializeField]
    Text text;

    private void Update()
    {
        text.text = GameManager.self.eventManager.GetTimeReadable();
    }
}
