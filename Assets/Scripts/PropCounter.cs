using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCounter : MonoBehaviour {

    public ParameterType parameter;
    public int[] valueStages;
    public GameObject[] objectsToMakeVisible;

    public bool inversed;

    private void Start()
    {
        GameManager.self.eventManager.parameters[parameter].OnChanged += ParameterChanged;
        GameManager.self.OnRestartGame += ResetGame;
    }

    private void ResetGame()
    {
        foreach(var ob in objectsToMakeVisible)
        {
            ob.gameObject.SetActive(false);
        }
        ParameterChanged(GameManager.self.eventManager.parameters[parameter]);
    }

    private void ParameterChanged(EffectParameter parameter)
    {
        
        if (parameter.parameterType == this.parameter)
        {
            for (int i = 0; i < valueStages.Length; i++)
            {
                if(inversed)
                {
                    if (parameter.currentValue <= valueStages[i])
                    {
                        if (objectsToMakeVisible.Length > i)
                        {
                            objectsToMakeVisible[i].gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (objectsToMakeVisible.Length > i)
                        {
                            objectsToMakeVisible[i].gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    if (parameter.currentValue >= valueStages[i])
                    {
                        if (objectsToMakeVisible.Length > i)
                        {
                            objectsToMakeVisible[i].gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (objectsToMakeVisible.Length > i)
                        {
                            objectsToMakeVisible[i].gameObject.SetActive(false);
                        }
                    }
                }
                
            }
        }
        
    }
}
