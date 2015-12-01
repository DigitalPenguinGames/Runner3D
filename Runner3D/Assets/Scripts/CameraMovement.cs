using UnityEngine;
using System.Collections;

public enum Looking {
    l3D, lTop, lProfile
};

public class CameraMovement : MonoBehaviour {

	public Looking cLooking = Looking.l3D;

	public float smoothTime = 0.3f;
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
    public GameObject canvasPort;
    public GameObject canvasLand;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			cLooking = Looking.l3D;
			moving = true;
			rotating = true;
            this.GetComponent<Camera>().orthographic = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			cLooking = Looking.lProfile;
			moving = true;
			rotating = true;
            this.GetComponent<Camera>().orthographic = true;
            this.GetComponent<Camera>().orthographicSize = 5.5f;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			cLooking = Looking.lTop;
			moving = true;
			rotating = true;
            this.GetComponent<Camera>().orthographic = false;
		}
		if (!moving && !rotating) return;
		// MOVING
		Vector3 targetPosition = Vector3.zero;
		Vector3 targetRotation = Vector3.zero;
		switch (cLooking) {
		    case Looking.l3D:
			    targetPosition = t3D;
			    targetRotation = r3D;
                canvasLand.SetActive(false);
                canvasPort.SetActive(true);
 			    break;
		    case Looking.lProfile:
			    targetPosition = tProfile;
			    targetRotation = rProfile;
			    if (transform.localEulerAngles.y > targetRotation.y + 360) targetRotation.y += 360;
                canvasLand.SetActive(true);
                canvasPort.SetActive(false);
			    break;
		    case Looking.lTop:
			    targetPosition = tTop;
			    targetRotation = rTop;
			    break;
		}
		if (moving) {
			transform.localPosition = Vector3.SmoothDamp (transform.localPosition, targetPosition, ref velocity, smoothTime);
			if (Vector3.Distance (transform.localPosition, targetPosition) < 0.0001) {
				moving = false;
				velocity = Vector3.zero;
			}
		}
		if (rotating) {
			Vector3 auxTargetRotation = targetRotation;
			if (Mathf.Abs(targetRotation.y - transform.localEulerAngles.y) > 180) {
				if (transform.localEulerAngles.y > targetRotation.y) auxTargetRotation.y += 360;
				else auxTargetRotation.y -= 360;
			}
			Vector3 rotation = Vector3.SmoothDamp (transform.localEulerAngles, auxTargetRotation, ref angularVelocity, smoothTime);
			transform.localEulerAngles = rotation;

			/*if (Vector3.Distance (transform.localEulerAngles, auxTargetRotation) < 0.00001) {
				rotating = false;
				angularVelocity = Vector3.zero;
			}*/
		}
	}
}
