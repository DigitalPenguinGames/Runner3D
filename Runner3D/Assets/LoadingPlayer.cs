using UnityEngine;
using System.Collections;

public class LoadingPlayer : MonoBehaviour {

    public float rotationSpeed = 10; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( rotationSpeed * Time.deltaTime , 0.0f , 0.0f );
	}

}
