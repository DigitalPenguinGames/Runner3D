using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject[] prefabs;
    public float speed = 1f;
    public float startToSpawn = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get the size of selected
        float lastPosition = 0;
        foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>()){
            float elementPosition = render.gameObject.transform.position.z + render.bounds.size.z;
            if (elementPosition > lastPosition){
                lastPosition = elementPosition;
            }
        }
        if (lastPosition < startToSpawn){
            GameObject instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(0, 0, lastPosition), Quaternion.identity) as GameObject;
            instance.transform.SetParent(transform);
        }
    }

    void FixedUpdate()
    {
        foreach (Transform trans in transform)
        {
            trans.Translate(0, 0, -speed * Time.fixedDeltaTime);
            if (trans.position.z + 12 < 0) Destroy(trans.gameObject);
        }
    }
}
