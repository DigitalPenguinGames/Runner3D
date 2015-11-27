using UnityEngine;
using System.Collections;

public class menuActor : MonoBehaviour {

    public bool isStart;
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
        }

        if (isStart) {
            Application.LoadLevel(1);
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    } 
}
