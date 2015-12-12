﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public bool touched = false;
	public float speed = 5;
	public float startToSpawn = 20;
	
	public bool tutorial = true;
	public float tutorialTime = 3;

	private ObstaclePattern[] obstacles;
	private float currentDificulty = 0;

	private int tutorialIndex = 0;
	private float tutorialElapsed = 0;

	void Start() {
		obstacles = gameObject.GetComponents<ObstaclePattern>();
	}

	void Update() {
		tutorialElapsed -= Time.deltaTime;
		// Get the size of selected
		float lastPosition = 0;
		foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>()){
			float elementPosition = render.gameObject.transform.position.z + render.bounds.size.z;
			if (elementPosition > lastPosition){
				lastPosition = elementPosition;
			}
		}
		lastPosition = lastPosition - 0.5f;
		if (lastPosition < startToSpawn) {
			if (tutorial) {
				if (tutorialElapsed < 0){
					if (touched) --tutorialIndex;
					touched = false;

					obstacles[tutorialIndex].spawn(transform,lastPosition);
					++tutorialIndex;
					if (tutorialIndex >= obstacles.Length) tutorial = false;
					tutorialElapsed = tutorialTime;
				}
				else {
					obstacles[0].spawn(transform,lastPosition);
				}
			}
			else {
				obstacles[Random.Range(0, obstacles.Length)].spawn(transform,lastPosition);
			}
		}
	}

	void FixedUpdate() {
		if (! Camera.main.GetComponent<CameraMovement>().movingOrRotating()) {
			foreach (Transform trans in transform) {
				trans.Translate(0, 0, -speed * Time.fixedDeltaTime);
				if (trans.position.z + 12 < 0) Destroy(trans.gameObject);
			}
		}
	}
}




/*
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public GameObject coin;
    public GameObject enemy2D;
    public GameObject enemy3D;

    private int tutorial = 0;
    private int passed = 2;

    public bool touched = false;
    public float speed = 1f;
    public float startToSpawn = 20;

    // Use this for initialization
    void Start() {
    }

    public void nextTuto() {
        tutorial += 1;
    }

    // Update is called once per frame
    void Update() {
        // Get the size of selected
        float lastPosition = 0;
        foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>()){
            float elementPosition = render.gameObject.transform.position.z + render.bounds.size.z;
            if (elementPosition > lastPosition){
                lastPosition = elementPosition;

            }
        }
         lastPosition = lastPosition - 0.5f;
        if (lastPosition < startToSpawn){

            if (tutorial < prefabs.Length+2)
            {   //TUTORIAL UPDATE
                //Debug.Log("TuToRiAl ----" + tutorial);

                if(tutorial < prefabs.Length-1){
                    GameObject instance = Instantiate(prefabs[tutorial], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                }
                else {
                    //Base de terra on hi haurà un enemic.
                    GameObject instance = Instantiate( prefabs[0] , new Vector3( 0 , 0 , lastPosition ) , Quaternion.identity ) as GameObject;
                    instance.transform.SetParent( transform );
                    //Enemic en 2D o 3D segons convingui
                    if ( Random.Range(0,2) % 2 == 0 ) {
                        GameObject instance1 = Instantiate( enemy2D , new Vector3( 0 , 0 , lastPosition ) , Quaternion.identity ) as GameObject;
                        instance1.transform.SetParent( transform );
                        instance1.transform.Translate( 0 , 0 , +6 );
                    } else {
                        GameObject instance2 = Instantiate( enemy3D , new Vector3( 0 , 0 , lastPosition ) , Quaternion.identity ) as GameObject;
                        instance2.transform.SetParent( transform );
                        instance2.transform.Translate( 0 , 0 , +6 );
                    }
                }


                if (touched) {
                    passed = 0;
                    touched = false;
                }
                else {
                    passed = passed + 1;
                    if (passed > 2) { 
                        passed = 0; 
                        nextTuto(); 
                    }
                }
            }
            else
            {   //REGULAR UPDATE
               // Debug.Log("Regular Game Update");
                int enemyRandom = Random.Range(0, 10);
                if (enemyRandom > 7)
                {
                    //Base de terra on hi haurà un enemic.
                    GameObject instance = Instantiate(prefabs[0], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                    //Enemic en 2D o 3D segons convingui
                    if (enemyRandom % 2 == 0)
                    {
                        GameObject instance1 = Instantiate(enemy2D, new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                        instance1.transform.SetParent(transform);
                        instance1.transform.Translate(0, 0, +6);
                    }
                    else
                    {
                        GameObject instance2 = Instantiate(enemy3D, new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                        instance2.transform.SetParent(transform);
                        instance2.transform.Translate(0, 0, +6);
                    }

                }
                else
                { //obstacle random sempre que no hauguem posat un enemic
                    GameObject instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                }

                int random = Random.Range(0, 2);
                bool spawnCoin = (random == 0);
                if (spawnCoin)
                { //coins 
                    random = Random.Range(0, 2);
                    Quaternion rotationQuat;
                    if (random % 2 == 0) rotationQuat = Quaternion.Euler(90, 0, 0);
                    else rotationQuat = Quaternion.Euler(0, 0, 90);
                    GameObject aux = new GameObject();
                    aux.transform.localPosition = new Vector3(0, 0, lastPosition);

                    GameObject mycoin = Instantiate(coin, new Vector3(0, 0.14f, lastPosition), rotationQuat) as GameObject;
                    aux.transform.SetParent(transform);
                    mycoin.transform.SetParent(aux.transform);

                }

            }

        }
    }

    void FixedUpdate() {
        if (! Camera.main.GetComponent<CameraMovement>().movingOrRotating()) {
            foreach (Transform trans in transform) {
                trans.Translate(0, 0, -speed * Time.fixedDeltaTime);
                if (trans.position.z + 12 < 0) Destroy(trans.gameObject);
            }
        }
    }
}
*/