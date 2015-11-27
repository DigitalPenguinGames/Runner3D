using UnityEngine;
using System.Collections;

public class TextConlorControl : MonoBehaviour {

    void Start()    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter()    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    void OnMouseExit()    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
