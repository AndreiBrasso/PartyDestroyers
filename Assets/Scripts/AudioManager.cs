using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Click,
    Random,
    CollectBeer,
    BackgroundPeople,
    BackgroundMusic,
    SweepFloor,
    TakeBeerFromFridge,
    BeerFalls,
    PeopleChanged,
    TableCrashes

}

public class AudioManager : MonoBehaviour {

    

    public AudioClip buttonClick;

    public void PlaySound()
    {

    }
}
