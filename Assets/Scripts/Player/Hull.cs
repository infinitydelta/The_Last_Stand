using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : MonoBehaviour {
	int maxHP = 500;
	int hp;
	float fireThreshold = .75f;
	int firecount = 0;
	float shakey = .2f;
	public bool dead = false;
	float timer;
	float explosionDelay = 0;
    float nextFirePercent = .15f;
    int deathExplosions = 0;


	public Text armor;
	public GameObject hitExplosion;
	public GameObject bigHitExplosion;
	public GameObject flames;
	public AudioClip[] explosions;
	public GameObject gameOver;

    public GameObject playerBroken;

	bool messaged = false;
	// Use this for initialization
	void Start () {
		hp = maxHP;
        //playerBroken = GameObject.Find("Ship Broken");
	}
	
	// Update is called once per frame
	void Update () {
		if (dead ) {
			timer += Time.deltaTime;
            if (timer >= explosionDelay && deathExplosions < 16)
            {
				float x = Random.Range(-3f, 3f);
				float y = Random.Range(-.8f, .8f);
				Instantiate(bigHitExplosion, new Vector3(x, y, 0), Quaternion.identity);
				explosionDelay +=.2f;
                deathExplosions++;
			}
            if (timer >= 3)
            {

                //instantiate broken ship prefab
                //destroy all enemy ships
                //destroy this?

                //Instantiate(ship)
                playerBroken.SetActive(true);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject enemy in enemies) {
                    enemy.GetComponent<Enemy>().endGameDeath();
                }
                Destroy(transform.parent.gameObject);
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
	
        //laser
		else {
			hp -= other.gameObject.GetComponent<enemylaser>().damage;
			if (!dead) {
				Camera.main.GetComponent<CameraShake>().shakeAmount = shakey;
				Camera.main.GetComponent<CameraShake>().shake = .2f;
			}
		}
		
        //create fire on ship
		if (hp < maxHP* fireThreshold && firecount < 6) {
            firecount++;
			Instantiate ( bigHitExplosion, new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, -4), transform.rotation);
			GameObject flamez = (GameObject) Instantiate ( flames, new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, -4), transform.rotation);
            flamez.transform.parent = transform.parent;
			audio.PlayOneShot(explosions[1]);
			shakey += .06f;
			if (shakey > .8f) shakey = .8f;
            fireThreshold -= nextFirePercent;
            nextFirePercent -= .02f; //every fire, 2% less to activate next one
		
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
