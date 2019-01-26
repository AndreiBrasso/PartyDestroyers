using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsAssets : MonoBehaviour {	

    [SerializeField]
    GraphicElement[] graphicElements;

    public Dictionary<GraphicTypes, GraphicElement> library;

    private void Awake()
    {
        if(library == null)
        {
            library = new Dictionary<GraphicTypes, GraphicElement>();
        }

        foreach(var g in graphicElements)
        {
            if (!library.ContainsKey(g.graphicType))
            {
                library.Add(g.graphicType, g);
            }
        }
        
    }
}
