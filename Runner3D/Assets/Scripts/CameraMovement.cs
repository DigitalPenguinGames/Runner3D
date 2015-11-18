using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 angularVelocity = Vector3.zero;
	private bool moving = true;
	private bool rotating = true;
	public Vector3 t3D;
	public Vector3 r3D;
	public Vector3 tTop;
	public Vector3 rTop;
	public Vector3 tProfile;
	public Vector3 rProfile;

	public Looking cLooking = Looking.l3D;

	public enum Looking {
		l3D, lTop, lProfile
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			cLooking = Looking.l3D;
			moving = true;
			rotating = true;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			cLooking = Looking.lProfile;
			moving = true;
			rotating = true;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			cLooking = Looking.lTop;
			moving = true;
			rotating = true;
		}
		if (!moving && !rotating) return;
		// MOVING
		Vector3 targetPosition = Vector3.zero;
		Vector3 targetRotation = Vector3.zero;
		switch (cLooking) {
		case Looking.l3D:
			targetPosition = t3D;
			targetRotation = r3D;
			break;
		case Looking.lProfile:
			targetPosition = tProfile;
			targetRotation = rProfile;
			break;
		case Looking.lTop:
			targetPosition = tTop;
			targetRotation = rTop;
			break;
		}
		if (moving) {
			transform.localPosition = Vector3.SmoothDamp (transform.localPosition, targetPosition, ref velocity, smoothTime);
			if (Vector3.Distance (transform.localPosition, targetPosition) < 0.01) {
				moving = false;
			}
		}
		if (rotating) {
			transform.localEulerAngles = Vector3.SmoothDamp (transform.localEulerAngles, targetRotation, ref angularVelocity, smoothTime);
			if (Vector3.Distance (transform.localEulerAngles, targetRotation) < 1) {
				rotating = false;
			}
		}
	}
}
