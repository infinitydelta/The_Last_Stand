using UnityEngine;
using System.Collections;

public class BrokenWhole : MonoBehaviour {
	float negX = -1.5f;
    public float oldx, oldy = 0;

	public AudioClip[] explosions;
    public bool playExplosion = true;
	// Use this for initialization
	void Start () {
		Camera.main.GetComponent<Score>().addScore(10);
		int chance = Random.Range(1, 100);
		if (chance > 50 && playExplosion) {
		
			int i = Random.Range(0, 1);
            audio.volume = .1f;
			audio.PlayOneShot(explosions[i]);
		}

        Destroy(this.gameObject, 11);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate((new Vector3(negX + oldx, oldy,0)) * Time.deltaTime, Space.World);
        oldx *= .999f;
        oldy *= .999f;
		//transform.Translate((new Vector3(negX,0,0)) * Time.deltaTime);
	}
}
