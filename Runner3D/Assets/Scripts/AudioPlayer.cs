using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

    public AudioClip[] jump;
    public AudioClip[] coins;
    public AudioClip[] crash;

    public float minVol, maxVol;

    private AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playCoinSound()
    {
        float vol = Random.Range(minVol, maxVol);
        source.PlayOneShot(coins[Random.Range(0, coins.Length)], vol);
    }

    public void playCrashSound()
    {
        float vol = Random.Range(minVol, maxVol);
        source.PlayOneShot(crash[Random.Range(0, crash.Length)], vol);
    }

    public void playJumpSound()
    {
        float vol = Random.Range(minVol, maxVol);
        source.PlayOneShot(jump[Random.Range(0, jump.Length)], vol);
    }
}
