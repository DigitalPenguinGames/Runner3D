using UnityEngine;
using System.Collections;

public class menuActor : MonoBehaviour {

    public bool isStart;
    public bool isCredits;
    public bool isQuit;

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnMouseUp() {
        
        if (isQuit) {
            Application.Quit();
            GetComponent<Renderer>( ).material.color = Color.cyan;
        }

        if ( isStart ) {
            Application.LoadLevel( 1 );
            GetComponent<Renderer>( ).material.color = Color.cyan;
        }

        if ( isCredits ) {
            Application.LoadLevel( 2 );
            GetComponent<Renderer>( ).material.color = Color.cyan;
        }

    } 
}
