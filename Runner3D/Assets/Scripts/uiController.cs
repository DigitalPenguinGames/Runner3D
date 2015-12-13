using UnityEngine;
using System.Collections;

public class uiController : MonoBehaviour {

	public float time = 0;
	public float coins = 0;
	public bool run = false;
    private GameObject player;

    public GameObject ui;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			if (!Camera.main.GetComponent<CameraMovement>().movingOrRotating())
				time += Time.deltaTime;
			//coins += Time.deltaTime;
            coins = player.GetComponent<PlayerBehaviour>().coins;
		}
		foreach (uiUpdater up in ui.GetComponentsInChildren<uiUpdater>()) {
			up.setCoins(Mathf.FloorToInt(coins));
			up.setTime(time);
		}
	}
}
