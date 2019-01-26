using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphicTypes
{
    Table,
    Fridge
}

public class GraphicElement : MonoBehaviour {
    

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    int currentIndex = 0;

    public GraphicTypes graphicType;

    public void SwitchGraphic(int graphicIndex)
    {
        if(graphicIndex < sprites.Length)
        {
            currentIndex = graphicIndex;
            spriteRenderer.sprite = this.sprites[graphicIndex];
        }
    }

    public void SetNextElement()
    {
        SwitchGraphic(currentIndex + 1);
    }

    public void SetRandomGraphic()
    {
        int newIndex = Random.Range(0, sprites.Length - 1);
        SwitchGraphic(newIndex);
    }
}
