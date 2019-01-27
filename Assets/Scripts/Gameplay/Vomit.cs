using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vomit : MonoBehaviour {

    public GameObject broom;

	// Use this for initialization
	void Start () {
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue-=5;
        GameManager.self.OnRestartGame += RestartGame;
    }

    private void OnDestroy()
    {
        GameManager.self.OnRestartGame -= RestartGame;
    }

    private void RestartGame()
    {
        Destroy(this.gameObject);
    }
    bool isCleaning = false;
    private void Cleanup()
    {
        isCleaning = true;
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue += 3;
        GameManager.self.eventManager.parameters[ParameterType.Fun].currentValue -= 5;

        Destroy(this.gameObject,2.0f);
    }

    private void OnMouseDown()
    {
        if (!isCleaning)
        {
            this.broom.gameObject.SetActive(true);
            Cleanup();
        }
       
    }

}
