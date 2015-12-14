using UnityEngine;
using System.Collections;

public class ObstaclePattern : MonoBehaviour {
	
	public int indexTutorial;
	public string description;
	public GameObject cartel;
	public GameObject[] obstacles;
	public bool tutorial = true;
	public float dificulty;

	public void spawn(Transform parent, float iniPosition, bool inTutorial) {
		float lastPosition = iniPosition;

		if (inTutorial && cartel != null) {
			GameObject instance = Instantiate(cartel, new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
			instance.transform.SetParent(transform);
			lastPosition = getLastPosition(lastPosition, instance);
		}

		for (int i = 0; i < obstacles.Length; ++i) {
			GameObject instance = Instantiate(obstacles[i], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
			instance.transform.SetParent(transform);
			lastPosition = getLastPosition(lastPosition, instance);
		}
	}

	public float getLastPosition(float lastPosition, GameObject instance) {
		foreach (Renderer render in instance.GetComponentsInChildren<Renderer>()){
			float elementPosition = render.gameObject.transform.position.z + render.bounds.size.z;
			if (elementPosition > lastPosition){
				lastPosition = elementPosition;
			}
		}
		lastPosition = lastPosition - 0.5f;
		return lastPosition;
	}
	
}
