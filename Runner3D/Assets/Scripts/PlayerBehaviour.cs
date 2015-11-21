using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    public Camera cam;
    private Rigidbody rb;
    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    public float jumpSpeed = 0.02f;
    public float movementSpeed = 0.01f;
    public float rotationSpeed = 500.0f;

    public float playerVelocity = 0.0f;
    public float playerMaxSpeed = 0.06f;
    public float playerAcceleration = 0.051f;
    

    void Start()    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()    {

    }

    void FixedUpdate()    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);

        // if Costat
        if (cam.transform.position.x != 0){

            if (this.transform.position.y > 0.2){
                vel.y -= jumpSpeed/8;
            }
            else if (moveVertical != 0){
                vel.y += jumpSpeed;
            }
            if (vel.y >= 1) {
                vel.y = 1;
            }
        }

        else        {  //if camera 3D || camera vertical

            if (moveVertical != 0) vel.x = 0;
            else if (moveHorizontal > 0)            {
                vel.x += playerAcceleration;
                if (vel.x > playerMaxSpeed)                {
                    vel.x = playerMaxSpeed;
                }
            }
            else if (moveHorizontal < 0)            {
                vel.x -= playerAcceleration;
                if (vel.x < -playerMaxSpeed)                {
                    vel.x = -playerMaxSpeed;
                }
            }

            //no pressed
            if (moveHorizontal == 0)            {
                if (vel.x < 0)                {
                    vel.x += playerAcceleration / 4;
                    if (vel.x >= 0)                    {
                        vel.x = 0.0f;
                    }
                }
                else                {
                    vel.x -= playerAcceleration / 4;
                    if (vel.x <= 0)                    {
                        vel.x = 0.0f;
                    }
                }
            }
        }

        Vector3 finalPosition = new Vector3(this.transform.position.x + vel.x, this.transform.position.y + vel.y, this.transform.position.z + vel.z);
        if (finalPosition.x > 1) finalPosition.x = 1;
        if (finalPosition.x < -1) finalPosition.x = -1;
        if (finalPosition.y < 0.1f) finalPosition.y = 0.1f;
        this.transform.position = finalPosition;
    }

}