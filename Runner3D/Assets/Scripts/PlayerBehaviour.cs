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
	public ParticleSystem deathExplosion;

    public float playerVelocity = 0.0f;
    public float playerMaxSpeed = 0.06f;
    public float playerAcceleration = 0.051f;

	public int numberOfCoinsOnCollision = 1;

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
     private float minSwipeDistX = Screen.width/8;
     private float minSwipeDistY = Screen.width/10;
              
     /*private Vector2 startPos;
     private float swipeValue;
     private float swipeDistVertical;
     private float swipeDistHorizontal;*/

     void Update()   {
         /*swipeValue = 0;
         swipeDistVertical = 0;
         swipeDistHorizontal = 0;

        //#if UNITY_ANDROID
         if (Input.touchCount > 0) {
             
             Touch touch = Input.touches[0];

             switch (touch.phase)  {
                 
             case TouchPhase.Began:
                 startPos = touch.position;
                 break;

             case TouchPhase.Moved:
                     swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                     if (swipeDistVertical > minSwipeDistY) {
                        swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                     }
                     
                     swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                     if (swipeDistHorizontal > minSwipeDistX) {
                         swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                         
//                         if (swipeValue > 0)//right swipe
  //                       else if (swipeValue < 0)//left swipe
                     }
                  break;

             case TouchPhase.Ended:
                     swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                     if (swipeDistVertical > minSwipeDistY) {
                     //moveVertical = 1;
                        swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                     }
                     
                     swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                     if (swipeDistHorizontal > minSwipeDistX) {
                         //moveHorizontal = 1; 
                         swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                         
//                         if (swipeValue > 0)//right swipe
  //                       else if (swipeValue < 0)//left swipe
                     }
                 break;
             }
         }
          */
     }

    void FixedUpdate() {

		if (cam.GetComponent<CameraMovement>().movingOrRotating()) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        //if ( swipeValue > 0 && swipeDistHorizontal >= minSwipeDistX ) { moveHorizontal = 1; moveVertical = 0;}
        //if ( swipeValue < 0 && swipeDistHorizontal >= minSwipeDistX ) { moveHorizontal = -1; moveVertical = 0;}
		for (var i = 0; i < Input.touchCount; ++i) {
            if ( Input.GetTouch( i ).phase == TouchPhase.Began ) {
                if ( cam.GetComponent<CameraMovement>( ).cLooking == Looking.lProfile ) moveVertical = 1;
                else if ( Input.GetTouch( i ).position.x < Screen.width / 3 ) moveHorizontal = -1;
                else if ( Input.GetTouch( i ).position.x < Screen.width * 2 / 3 ) moveVertical = 1;
                else moveHorizontal = 1;
            }
            if (Input.GetTouch(i).phase == TouchPhase.Stationary) {
				if (Input.GetTouch(i).position.x < Screen.width/3) moveHorizontal = -1; 
                else if ( Input.GetTouch(i).position.x > Screen.width * 2/3 ) moveHorizontal = 1; 
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
            //Debug.Log("JUMPIN!!!!");
                audioScript.playJumpSound( );
                vel.y = jumpSpeed;
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
            if (this.transform.position.y > initPosition.y) {
                vel.y -= gravity;
            }
            else if (moveVertical != 0 )
            //else if(swipeValue > 0 && swipeDistVertical >= minSwipeDistY)
            {
                audioScript.playJumpSound();
                vel.y = jumpSpeed;
            }
            if (vel.y >= 1) {
                vel.y = 1;
            }


            if (moveHorizontal > 0)
            //if ( swipeValue > 0 && swipeDistHorizontal >= minSwipeDistX)
            {
                vel.x += playerAcceleration;
                if (vel.x > playerMaxSpeed)
                {
                    vel.x = playerMaxSpeed;
                }
            }
            else if (moveHorizontal < 0)
            //else if ( swipeValue < 0 && swipeDistHorizontal >= minSwipeDistX)
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
		if (!spawner.tutorial) {
			coins -= numberOfCoinsOnCollision;
			++numberOfCoinsOnCollision;
		}
		if (coins < 0) deathExplosions();
	}

	public void deathExplosions() {
		deathExplosion.transform.position = transform.position;
		deathExplosion.time = 0;
		deathExplosion.startDelay = 0.01f;
		deathExplosion.Play();
	}
}

