using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanel : MonoBehaviour {

    [SerializeField]
    Text mainText;

    [SerializeField]
    Text option1Text;

    [SerializeField]
    Text option2Text;

    [SerializeField]
    Text[] valuesText;

    private void Start()
    {
        GameManager.self.eventManager.OnEventChanged += NewGameEventSelected;
        foreach(var p in GameManager.self.eventManager.parameters)
        {
            UpdateValue(p.Value);
        }
        SetListeners();
    }

    private void OnDestroy()
    {
        GameManager.self.eventManager.OnEventChanged -= NewGameEventSelected;
        ClearListeners();
    }

    private void NewGameEventSelected(GameEvent newEvent)
    {
        mainText.text = newEvent.Text;
        option1Text.text = newEvent.Option1;
        option2Text.text = newEvent.Option2;
    }

    private void SetListeners()
    {
        for(int i = 0; i<valuesText.Length; i++)
        {
            GameManager.self.eventManager.parameters[(ParameterType)i].OnChanged += UpdateValue;    
        }
    }

    private void ClearListeners()
    {
        for (int i = 0; i < valuesText.Length; i++)
        {
            GameManager.self.eventManager.parameters[(ParameterType)i].OnChanged -= UpdateValue;
        }
    }

    public void UpdateValue(EffectParameter parameter)
    {
        if (parameter.parameterType == ParameterType.Time)
        {
            valuesText[(int)parameter.parameterType].text = parameter.parameterType + ": " + GameManager.self.eventManager.GetTimeReadable();
        }
        else
        {
            valuesText[(int)parameter.parameterType].text = parameter.parameterType + ": " + parameter.currentValue.ToString("F2");
        }
        
    }

    public void SelectOption(int option)
    {
        GameManager.self.eventManager.SelectOption(option);
    }

}
