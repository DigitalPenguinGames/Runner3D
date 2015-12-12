using UnityEngine;
using System.Collections;

public class ObstaclePattern : MonoBehaviour {
	
	public int indexTutorial;
	public string description;
	public GameObject[] obstacles;
	public float dificulty;

	public void spawn(Transform parent, float iniPosition) {
		float lastPosition = iniPosition;
		for (int i = 0; i < obstacles.Length; ++i) {
			GameObject instance = Instantiate(obstacles[i], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
			instance.transform.SetParent(transform);

			foreach (Renderer render in instance.GetComponentsInChildren<Renderer>()){
				float elementPosition = render.gameObject.transform.position.z + render.bounds.size.z;
				if (elementPosition > lastPosition){
					lastPosition = elementPosition;
				}
			}
			lastPosition = lastPosition - 0.5f;
		}
	}
}
