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
	private Vector3 targetPosition;
    public GameObject canvasPort;
    public GameObject canvasLand;

	public float duration = 0.7f;
	public float magnitude = 0.5f;
	public float intensity = 20;

    private float lastMovement = 5;
    public bool movingOrRotating() {
        return lastMovement < smoothTime; 
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lastMovement += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space)){
            if(cLooking != Looking.l3D){
                cLooking = Looking.l3D;
                moving = true;
			    rotating = true;
                lastMovement = 0;
                this.GetComponent<Camera>().orthographic = false;
            }
            else {
                cLooking = Looking.lProfile;
                moving = true;
			    rotating = true;
                lastMovement = 0;
                this.GetComponent<Camera>().orthographic = true;
                this.GetComponent<Camera>().orthographicSize = 5.5f;
            }
        }
       if ( Input.GetKeyDown( KeyCode.Alpha1 ) || Mathf.Abs( Input.acceleration.x ) < 0.3 ) {
        //if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) {
            if ( cLooking != Looking.l3D ) lastMovement = 0;
            cLooking = Looking.l3D;
			moving = true;
			rotating = true;
            this.GetComponent<Camera>().orthographic = false;
		} else 
        // if (Input.GetKeyDown(KeyCode.Alpha2)) {
	     if ( Input.GetKeyDown( KeyCode.Alpha2 ) || Mathf.Abs( Input.acceleration.x ) > 0.7 ) {
            if ( cLooking != Looking.lProfile ) lastMovement = 0;
            cLooking = Looking.lProfile;
			moving = true;
			rotating = true;
            this.GetComponent<Camera>().orthographic = true;
            this.GetComponent<Camera>().orthographicSize = 5.5f;
		}
/*		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			cLooking = Looking.lTop;
			moving = true;
			rotating = true;
            lastMovement = 0;
            this.GetComponent<Camera>().orthographic = false;
		}*/
		if (!moving && !rotating) return;
		// MOVING
		targetPosition = Vector3.zero;
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
		if (Input.GetKeyDown(KeyCode.Z)) shake();
	}

	IEnumerator Shake() {
		
		float elapsed = 0.0f;
		
		Vector3 originalCamPos = targetPosition;
		
		while (elapsed < duration) {
			originalCamPos = targetPosition;
			
			elapsed += Time.deltaTime;          
			
			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			// float x = Random.value * 2.0f - 1.0f;
			// float y = Random.value * 2.0f - 1.0f;
			float x = Mathf.Clamp(Mathf.PerlinNoise(percentComplete*intensity,0),0.0f, 1.0f) * 2.0f - 1;
			float y = Mathf.Clamp(Mathf.PerlinNoise(percentComplete*intensity,10),0.0f, 1.0f) * 2.0f - 1;
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(x+originalCamPos.x, y+originalCamPos.y, originalCamPos.z);
			
			yield return null;
		}
		
		Camera.main.transform.position = originalCamPos;

		Debug.Log("shaking");
	}

	public void shake() {
		StartCoroutine(Shake());
	}

}

