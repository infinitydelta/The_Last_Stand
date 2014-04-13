using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour {
	int maxHP = 500;
	int hp;
	float fireThreshold = .5f;
	int firecount = 0;
	float shakey = .2f;
	bool dead = false;
	float timer;
	float explosionDelay = 0;

	public GUIText armor;
	public GameObject hitExplosion;
	public GameObject bigHitExplosion;
	public GameObject flames;
	public AudioClip[] explosions;
	public GameObject gameOver;

	bool messaged = false;
	// Use this for initialization
	void Start () {
		hp = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			timer += Time.deltaTime;
			if (timer >= explosionDelay ) {
				float x = Random.Range(-3f, 3f);
				float y = Random.Range(-.8f, .8f);
				Instantiate(bigHitExplosion, new Vector3(x, y, 0), Quaternion.identity);
				explosionDelay +=.2f;
			}
			if (timer >= 3 && messaged == false) {

				Instantiate(gameOver, new Vector3(.5f, .75f,0), Quaternion.identity);
				messaged = true;
				//Destroy(this);
			}
		}
		if (hp < 0) {
			hp = 0;
		}
		armor.color = new Color (1f, (float) hp/maxHP, (float) hp/maxHP);
		armor.text = "ARMOR " + hp;

		
	}
	
	void OnCollisionEnter2D( Collision2D other) {
		Instantiate(hitExplosion, new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, -4), transform.rotation);
		if (other.gameObject.CompareTag("enemy")) {
			hp -= 50;
			audio.PlayOneShot(explosions[Random.Range(0,1)]);
			if (!dead) {
				Camera.main.GetComponent<CameraShake>().shakeAmount = .5f;
				Camera.main.GetComponent<CameraShake>().shake = .5f;
			}
		}
	

		else {
			hp -= other.gameObject.GetComponent<enemylaser>().damage;
			if (!dead) {
				Camera.main.GetComponent<CameraShake>().shakeAmount = shakey;
				Camera.main.GetComponent<CameraShake>().shake = .2f;
			}
		}
		
		if (hp < maxHP* fireThreshold) {
			Instantiate ( bigHitExplosion, new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, -4), transform.rotation);
			Instantiate ( flames, new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, -4), transform.rotation);
			audio.PlayOneShot(explosions[1]);
			shakey += .06f;
			if (shakey > .8f) shakey = .8f;
			fireThreshold *= .75f;
		
		}
		if (hp <=0) {
			death();
		}
	}
	
	void death() {
		dead = true;
		Destroy(GameObject.Find ("shield"));
		Destroy (GameObject.Find ("engine flame"));
		GameObject[] turrets = GameObject.FindGameObjectsWithTag ("turret");
		foreach (GameObject turret in turrets) {
			if (turret.GetComponent<Ship>()) {
				Destroy(turret.GetComponent<Ship>());
			}
			if (turret.GetComponent<AutoGun>()) {
				Destroy(turret.GetComponent<AutoGun>());
			}
		}
		for (int i = 0; i < 5; i++ ) {

		}
	}
}
