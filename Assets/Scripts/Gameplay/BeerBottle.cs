using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeerBottle : MonoBehaviour 
{
       
    bool finishedInteraction;

    public Rigidbody2D rbody;

    public GameObject ciob1;
    public GameObject ciob2;

    private void Start()
    {
        GameManager.self.eventManager.parameters[ParameterType.Booze].currentValue--;
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue--;
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        gameObject.transform.position = pz; 
    }    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !finishedInteraction)
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            gameObject.transform.position = pz;
        }
        else
        {
            finishedInteraction = true;
            rbody.WakeUp();
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Person>() != null)
        {
            collision.collider.GetComponent<Person>().Drink();
            Destroy(this.gameObject);
        }
        else if(!Input.GetMouseButton(0))
        {
            var c1 = Instantiate(ciob1, this.transform.position, Quaternion.identity);
            c1.transform.SetParent(this.transform.parent);
            c1.transform.localScale = Vector3.one;

            var c2 = Instantiate(ciob2, this.transform.position, Quaternion.identity);
            c2.transform.SetParent(this.transform.parent);
            c2.transform.localScale = Vector3.one;

            Destroy(this.gameObject);
            
        }
    }
}
