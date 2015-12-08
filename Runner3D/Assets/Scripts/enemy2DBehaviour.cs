using UnityEngine;
using System.Collections;

public class enemy2DBehaviour : MonoBehaviour {

    private Camera cam;
    private Vector3 initPosition;
    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    public float gravity = 0.1f;
    private GameObject player;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindObjectOfType<Camera>();
        initPosition = transform.localPosition;
	}

    void FixedUpdate() {

        
       // transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        Vector3 finalPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z);;
        // if 3d
        if (cam.GetComponent<CameraMovement>().cLooking == Looking.l3D) {
            if (this.transform.position.y > initPosition.y) {
                vel.y -= gravity;
                finalPosition = new Vector3(this.transform.position.x + vel.x, this.transform.position.y + vel.y, this.transform.position.z + vel.z);
            }
        }

        else {  
            finalPosition.y = player.transform.position.y;
        }

        if (finalPosition.x > 1) finalPosition.x = 1;
        if (finalPosition.x < -1) finalPosition.x = -1;
        if (finalPosition.y < initPosition.y) finalPosition.y = initPosition.y;
        this.transform.position = finalPosition;
    }
}