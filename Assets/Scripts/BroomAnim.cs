using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAnim : MonoBehaviour {

    public float amount = 30f;
    public float speed = 1;
    float rV = 0;

    private void Start()
    {
        rV = Random.value;
    }

    // Update is called once per frame
    void Update () {
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time*(speed+rV) + rV) * amount);
	}
}
