using UnityEngine;
using System.Collections;

public class ObstacleDirectional : MonoBehaviour {

    public float smoothTime = 0.2f;
    public GameObject[] profile;
    public GameObject camera;

    private float alfaVelocityProfile = 0;
    private float alfaVelocity3D = 0;
    private float alfaVelocityTop = 0;

    public Looking cLooking;

    // Use this for initialization
    void Start () {
        camera = GameObject.FindObjectOfType<Camera>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        cLooking = camera.GetComponent<CameraMovement>().cLooking;
        float targetProfile = 1;
        float target3D = 1;
        float targetTop = 1;
        switch (cLooking) {
            case Looking.l3D:
                targetProfile = 1;
                target3D = 0;
                targetTop = 1;
                break;
            case Looking.lProfile:
                targetProfile = 0;
                target3D = 1;
                targetTop = 1;
                break;
            case Looking.lTop:
                targetProfile = 1;
                target3D = 1;
                targetTop = 0;
                break;
        }
        foreach (GameObject goProfile in profile) {
            Color colorAux = goProfile.GetComponent<MeshRenderer>().material.color;
            float finalAlpha = Mathf.SmoothDamp(goProfile.GetComponent<Renderer>().material.color.a, targetProfile, ref alfaVelocityProfile, smoothTime);
            colorAux.a = finalAlpha;
            goProfile.GetComponent<MeshRenderer>().material.color = colorAux;
            if (finalAlpha > 0.8) {
                goProfile.GetComponent<BoxCollider>().enabled = true;
                goProfile.GetComponent<MeshRenderer>().enabled = true;
            }
            else if (finalAlpha < 0.2) {
                goProfile.GetComponent<BoxCollider>().enabled = false;
                goProfile.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
