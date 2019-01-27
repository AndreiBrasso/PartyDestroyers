using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLogic : MonoBehaviour
{

    [SerializeField] ParameterType parameterType;
    [SerializeField] int[] changeValues;
    [SerializeField] GraphicElement graphicElement;

    int lastGraphic;
    public AudioSource aSource;

    private void Start()
    {
        GameManager.self.eventManager.parameters[parameterType].OnChanged += ValueChanged;
    }

    private void OnDestroy()
    {
        GameManager.self.eventManager.parameters[parameterType].OnChanged -= ValueChanged;
    }

    
    private void ValueChanged(EffectParameter parameter)
    {
        int value = (int)parameter.currentValue;
        for(int i = 0; i<changeValues.Length; i++)
        {
            if (value < changeValues[i])
            {
                graphicElement.SwitchGraphic(i);
                if(lastGraphic != i)
                {
                    if(aSource!=null)aSource.Play();
                }
                lastGraphic = i;
                break;
            }
        }
    }
}
