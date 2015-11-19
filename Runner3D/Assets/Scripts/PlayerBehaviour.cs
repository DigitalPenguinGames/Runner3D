using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    public float rotationSpeed = 500.0f;
    public float movementSpeed = 10.0f;
    public Vector3 jumpPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 leftPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 rightPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 centerPos = new Vector3(0.0f, 0.0f, 0.0f);


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);

        //if camera 3D || camera vertical
        if (moveVertical != 0) rb.MovePosition(centerPos);
        else if (moveHorizontal > 0) rb.MovePosition(rightPos);
        else if (moveHorizontal < 0) rb.MovePosition(leftPos);
        
        //if camera Lateral
        //if (moveVertical != 0 || moveHorizontal != 0){
           // rb.MovePosition(jumpPos);
        //}

//        rb.AddForce(movement * movementSpeed);
    }

}
