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
        this.GetComponent<Light>().range = 3.2f + Mathf.Max(Mathf.Sin(timer/2), Mathf.Sin(timer));


    }
}
