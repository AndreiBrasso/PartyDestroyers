using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {


    [SerializeField]
    Sprite[] danceAnimation;
    [SerializeField]
    Sprite[] drinkAnimation;
    [SerializeField]
    Sprite[] standAnimation;
    [SerializeField]
    Sprite[] vomitAnimation;

    [SerializeField]
    float funThreshold = 15;

    [SerializeField]
    SpriteRenderer sR;
    float drunkMeter = 0;

    [SerializeField]
    Vomit vomit;

    [SerializeField] AudioSource aSource;

    private void Start()
    {
        Stand();
        GameManager.self.eventManager.parameters[ParameterType.Fun].OnChanged += FunChanged;
    }

    private void FunChanged(EffectParameter parameter)
    {
        if(this.gameObject.activeInHierarchy)
            if(parameter.parameterType == ParameterType.Fun)
            {
                if (parameter.currentValue > funThreshold)
                {
                    Dance();
                }
                else
                {
                    Stand();
                }
            }
    }

    private void OnEnable()
    {
        drunkMeter = 0;
    }

    public void Drink()
    {
        drunkMeter++;
        GameManager.self.eventManager.parameters[ParameterType.Fun].currentValue++;
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue++;
        StopAllCoroutines();
        StartCoroutine(Animate(drinkAnimation,danceAnimation.Length * 4,null));
        aSource.Play();
    }

    public void Dance()
    {
        if(drunkMeter < 3)
        {
            if (drunkMeter > 0)
            {
                drunkMeter++;
            }
            StopAllCoroutines();
            StartCoroutine(Animate(danceAnimation, danceAnimation.Length * 10,null));
        }
        else
        { 
            Vomit();
        }
        
    }

    public void Stand()
    {
      
        StopAllCoroutines();
        StartCoroutine(Animate(standAnimation, standAnimation.Length,null));
    
    }
    public void Vomit()
    {
        drunkMeter = 2;
        
        StopAllCoroutines();
        StartCoroutine(Animate(vomitAnimation, vomitAnimation.Length, delegate()
        {
            var v = Instantiate(vomit);
            v.transform.SetParent(this.transform.parent.parent);
            v.transform.position = this.transform.position;
        }
        ));
    }

    public IEnumerator Animate(Sprite[] animation, int animationFrames, System.Action completed)
    {
        for(int i = 0; i<animationFrames; i++)
        {
            this.sR.sprite = animation[i % animation.Length];
            yield return new WaitForSeconds(GameManager.self.animationDelay);
        }
        if (completed != null) completed();
        Stand();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    private void OnMouseDown()
    {
        Dance();
    }

}
