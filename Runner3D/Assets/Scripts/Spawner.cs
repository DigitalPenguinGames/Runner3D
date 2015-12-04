using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public GameObject coin;
    public GameObject enemy2D;
    public GameObject enemy3D;

    public float speed = 1f;
    public float startToSpawn = 20;

    // Use this for initialization
    void Start() {
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
        if (lastPosition < startToSpawn){

            int enemyRandom = Random.Range(0, 10);
            if (enemyRandom > 7) {
                GameObject instance = Instantiate(prefabs[0], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                instance.transform.SetParent(transform);

                if (enemyRandom % 2 == 0) {
                    GameObject instance1 = Instantiate(enemy2D, new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                    instance1.transform.SetParent(transform);
                    instance1.transform.Translate(0, 0, +6);
                }
                else {
                    GameObject instance2 = Instantiate(enemy3D, new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                    instance2.transform.SetParent(transform);
                    instance2.transform.Translate(0, 0, +6);
                }

            }
            else {
                GameObject instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
                instance.transform.SetParent(transform);
            }

            int random = Random.Range(0, 2);
            bool spawnCoin = (random == 0);
            if (spawnCoin) {
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

    void FixedUpdate() {
        if (! Camera.main.GetComponent<CameraMovement>().movingOrRotating()) {
            foreach (Transform trans in transform) {
                trans.Translate(0, 0, -speed * Time.fixedDeltaTime);
                if (trans.position.z + 12 < 0) Destroy(trans.gameObject);
            }
        }
    }
}
