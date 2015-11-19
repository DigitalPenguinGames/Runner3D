using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float zPosition = 0;
		/*Transform selected = transform;
		foreach(Transform trans in gameObject.GetComponentsInChildren<Transform>()) {
			if (trans.position.z > zPosition) {
				zPosition = trans.position.z;
				selected = trans;
			}
		}*/
		// Get the size of selected
		float lastPosition = zPosition;
		foreach(Renderer render in gameObject.GetComponentsInChildren<Renderer>()) {
			if (render.bounds.max.z > lastPosition) {
				lastPosition = render.bounds.max.z;
			}
		}
		if (lastPosition < 12) {
			Debug.Log ("Last position " + lastPosition + " zPosition " + zPosition);
			GameObject instance = Instantiate(prefabs[Random.Range(0,prefabs.Length)], new Vector3(0,0,lastPosition+0.5f), Quaternion.identity) as GameObject;
			instance.transform.SetParent(transform);
		}
	}

	void FixedUpdate() {
		foreach(Transform trans in transform) {
			trans.Translate(0,0,-speed*Time.fixedDeltaTime);
			if (trans.position.z + 12 < 0) Destroy(trans.gameObject);
		}
	}
}

