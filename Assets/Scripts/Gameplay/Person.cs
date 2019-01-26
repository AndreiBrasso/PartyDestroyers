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
    SpriteRenderer sR;
    float drunkMeter = 0;

    public void Drink()
    {
        drunkMeter++;
        GameManager.self.eventManager.parameters[ParameterType.Fun].currentValue++;
        GameManager.self.eventManager.parameters[ParameterType.House].currentValue++;
        StopAllCoroutines();
        StartCoroutine(Animate(drinkAnimation,danceAnimation.Length * 4));
    }

    public void Dance()
    {
        StopAllCoroutines();
        StartCoroutine(Animate(danceAnimation, danceAnimation.Length * 10));
    }

    public void Stand()
    {
        StopAllCoroutines();
        StartCoroutine(Animate(standAnimation,standAnimation.Length));
    }

    public IEnumerator Animate(Sprite[] animation, int animationFrames)
    {
        for(int i = 0; i<animationFrames; i++)
        {
            this.sR.sprite = animation[i % animation.Length];
            yield return new WaitForSeconds(GameManager.self.animationDelay);
        }
        Stand();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            Dance();
        }
    }

}
