using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class uiUpdater : MonoBehaviour {

	public Text coins;
	public Text time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCoins(int c) {
		coins.text = c.ToString();
	}

	public void setTime(float t) {
		time.text =  string.Format("{0:#0}:{1:00}.{2:0}",
		                           Mathf.Floor(t / 60),//minutes
		                           Mathf.Floor(t) % 60,//seconds
		                           Mathf.Floor((t*10) % 10));//deciseconds;
	}
}
