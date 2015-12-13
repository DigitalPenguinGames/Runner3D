using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    public int coins;
    private Camera cam;
    private Vector3 initPosition;
    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);
    private GameObject AudioManager;
    private AudioPlayer audioScript;

    public float jumpSpeed = 0.2f;
    public float gravity = 0.1f;
    public float movementSpeed = 0.01f;
    public float rotationSpeed = 500.0f;
    public ParticleSystem explosion;


    public float playerVelocity = 0.0f;
    public float playerMaxSpeed = 0.06f;
    public float playerAcceleration = 0.051f;

	public int numberOfCoinsOnCollision = 10;

    int getCoins() {
        return coins;
    }

    void Explode()
    {
        Debug.Log("CoinPlosion!!!!!!!!!!!");
        explosion.time = 0;
        explosion.startDelay = 0.01f;
        explosion.Play();

        //Instantiate(explosion, this.transform.position, Quaternion.identity);

    }

    void Start()
    {
        coins = 0;
        AudioManager = GameObject.Find("Audio");
        audioScript = (AudioPlayer) AudioManager.GetComponent(typeof(AudioPlayer));
        
        //explosion.Play();
        cam = GameObject.FindObjectOfType<Camera>();
        initPosition = transform.localPosition;

    }

    void OnCollisionEnter(Collision col)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
		if (cam.GetComponent<CameraMovement>().movingOrRotating()) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				if (cam.GetComponent<CameraMovement>().cLooking == Looking.lProfile) moveVertical = 1;
				else if (Input.GetTouch(i).position.x < Screen.width/3) moveHorizontal = -1; 
				else if (Input.GetTouch(i).position.x < Screen.width*2/3) moveVertical = 1; 
				else moveHorizontal = 1; 
			}
		}

        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);

        // if Costat
        if (cam.GetComponent<CameraMovement>().cLooking == Looking.lProfile)
        {

            if (this.transform.position.y > initPosition.y)
            {
                vel.y -= gravity;
            }
            else if (moveVertical != 0)
            {
                audioScript.playJumpSound( );
                vel.y += jumpSpeed;
            }
            if (vel.y >= 1)
            {
                vel.y = 1;
            }

            Vector3 fixedPosition = new Vector3( 0 , this.transform.position.y, this.transform.position.z);
            this.transform.position = fixedPosition;
        }

        else
        {  //if camera 3D || camera vertical

            //if (moveVertical != 0) vel.x = 0;
            if (this.transform.position.y > initPosition.y)
            {
                vel.y -= gravity;
            }
            else if (moveVertical != 0)
            {
                audioScript.playJumpSound();
                vel.y += jumpSpeed;
            }
            if (vel.y >= 1)
            {
                vel.y = 1;
            }


            if (moveHorizontal > 0)
            {
                vel.x += playerAcceleration;
                if (vel.x > playerMaxSpeed)
                {
                    vel.x = playerMaxSpeed;
                }
            }
            else if (moveHorizontal < 0)
            {
                vel.x -= playerAcceleration;
                if (vel.x < -playerMaxSpeed)
                {
                    vel.x = -playerMaxSpeed;
                }
            }

            //no pressed
            if (moveHorizontal == 0)
            {
                if (vel.x < 0)
                {
                    vel.x += playerAcceleration / 4;
                    if (vel.x >= 0)
                    {
                        vel.x = 0.0f;
                    }
                }
                else
                {
                    vel.x -= playerAcceleration / 4;
                    if (vel.x <= 0)
                    {
                        vel.x = 0.0f;
                    }
                }
            }
        }

        Vector3 finalPosition = new Vector3(this.transform.position.x + vel.x, this.transform.position.y + vel.y, this.transform.position.z + vel.z);
        if (finalPosition.x > 1) finalPosition.x = 1;
        if (finalPosition.x < -1) finalPosition.x = -1;
        if (finalPosition.y < initPosition.y) finalPosition.y = initPosition.y;
        this.transform.position = finalPosition;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Coin")
        {

            //Do CoinStuff
            //TODO only do it if same lookat
            if ((cam.GetComponent<CameraMovement>().cLooking == Looking.l3D && other.gameObject.transform.rotation.x > 0)
                || (cam.GetComponent<CameraMovement>().cLooking == Looking.lProfile && other.gameObject.transform.rotation.x <= 0))
            {
                Debug.Log(other.gameObject.transform.rotation.x);
                ++coins;
                audioScript.playCoinSound();
                explosion.transform.position = this.transform.position;
                other.enabled = false;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                Explode();
            }
        } else {

            Spawner spawner = FindObjectOfType<Spawner>();
 
            if (other.gameObject.tag == "cameraDependentObstacle3D") {

                if (cam.GetComponent<CameraMovement>().cLooking == Looking.l3D) {
					collision(spawner,other);
                }
            }
            else if (other.gameObject.tag == "cameraDependentObstacleProfile") {

                if (cam.GetComponent<CameraMovement>().cLooking == Looking.lProfile) {
					collision(spawner,other);
                }
            }
            else if (other.gameObject.tag == "Obstacle")
            {
				collision(spawner,other);
            }
        }
    }

	private void collision(Spawner spawner, Collider other) {
		spawner.touched = true;
		
		other.enabled = false;
		audioScript.playCrashSound();
		cam.GetComponent<CameraMovement>().shake();
		if (!spawner.tutorial) coins -= numberOfCoinsOnCollision;
		if (coins < 0) Explode();
	}
}