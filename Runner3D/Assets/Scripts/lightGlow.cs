using UnityEngine;
using System.Collections;

public class lightGlow : MonoBehaviour {

    public float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {

        float delta = Time.fixedDeltaTime;
        timer += delta;
        this.GetComponent<Light>().range = 3 + Mathf.Max(0.3f*Mathf.Sin(timer/2), Mathf.Sin(timer));


    }
}
