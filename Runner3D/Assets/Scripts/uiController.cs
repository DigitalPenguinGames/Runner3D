using UnityEngine;
using System.Collections;

public class uiController : MonoBehaviour {

	public float time = 0;
	public float coins = 0;
	public bool run = false;

	public GameObject ui;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			time += Time.deltaTime;
			coins += Time.deltaTime;
		}
		foreach (uiUpdater up in ui.GetComponentsInChildren<uiUpdater>()) {
			up.setCoins(Mathf.FloorToInt(coins));
			up.setTime(time);
		}
	}
}
