using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour {

    bool hasResources
    {
        get
        {
            return GameManager.self.eventManager.parameters[ParameterType.Booze].currentValue > 1;
        }
    }

    [SerializeField]
    BeerBottle beerBottle;

    private void OnMouseDown()
    {
        if(hasResources)
        {
            var bBottle = Instantiate(beerBottle);
            bBottle.transform.SetParent(this.transform.parent);
            bBottle.transform.localScale = Vector3.one * 2;
        }
    }
}
