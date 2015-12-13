using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	

	public GameObject player;
	public GameObject spawner;
	private bool paused = false;
	private bool dead = false;

	private float elapsed = 0;

    void Start(){

    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !dead) {
			if (!paused) {
				gameObject.AddComponent<PauseMenu>();
				paused = true;
				Time.timeScale = 0;
			}
			else quitPause();
		}
		if (player.GetComponent<PlayerBehaviour>().coins < 0 && !dead) {
			dead = true;
		}
		if (dead) {
			elapsed += Time.deltaTime;
			if (elapsed >= 0.3) {
				gameObject.AddComponent<DeathMenu>();
				elapsed = 0;
				Time.timeScale = 0;
			}
		}
	}

	public void quitPause() {
		if (GetComponentInParent<PauseMenu>() != null) Destroy(GetComponentInParent<PauseMenu>());
		Time.timeScale = 1;
		paused = false;
	}	

	public void quitDeath() {
		if (GetComponentInParent<DeathMenu>() != null) Destroy(GetComponentInParent<DeathMenu>());
		Time.timeScale = 1;
		dead = false;
		spawner.GetComponent<Spawner>().clearRoad();
		player.GetComponent<PlayerBehaviour>().coins = 0;
		GetComponent<uiController>().time = 0;
	}
}
