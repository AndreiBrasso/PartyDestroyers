using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vomit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue-=5;
    }

    private void Cleanup()
    {
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue += 3;
        GameManager.self.eventManager.parameters[ParameterType.Fun].currentValue -= 5;
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        Cleanup();
    }

}
