using UnityEngine;
using System.Collections;

public class creditsLettersBehaviour : MonoBehaviour {

    public float rotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

     void FixedUpdate() {

         this.transform.rotation = ( Quaternion.Euler( 0.0f , 0.0f , ( Mathf.Sin( rotationSpeed * Time.time) ) ) );
        
    }
}
