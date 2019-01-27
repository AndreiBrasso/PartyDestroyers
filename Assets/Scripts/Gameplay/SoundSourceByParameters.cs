using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourceByParameters : MonoBehaviour {

    public ParameterType paramType;

    public AudioSource audioSource;
    public AnimationCurve animCurve;

    public bool isOnlyVolumeChanged;
    public bool isTrigger;
     

    float getVolume
    {
        get
        {
            float rawVal = GameManager.self.eventManager.parameters[paramType].currentValue;
            float xV = 0;
            if (paramType == ParameterType.People)
            {
                xV = rawVal / 30;
            }
            else
            {
                xV = rawVal;
            }
            return animCurve.Evaluate(xV);
        }
    }

    private void Start()
    {
        GameManager.self.eventManager.parameters[this.paramType].OnChanged += ParameterChanged;
    }

    private void OnDestroy()
    {
        GameManager.self.eventManager.parameters[this.paramType].OnChanged -= ParameterChanged;
    }

    private void ParameterChanged(EffectParameter param)
    {
        if(isOnlyVolumeChanged) audioSource.volume = getVolume;
        if (isTrigger)
        {
            audioSource.Play();
        }
    }
}
