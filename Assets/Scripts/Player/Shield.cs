using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public GameObject hitExplosion;
	public float shield;
	public GUIText shieldHUD;
	public GUIText energyHUD;
	public AudioClip[] sounds;
	float timer = 0;
	float regenCD = .75f;
	float regenAmount = 10; //per s
	int max = 100;
	float fade;
	float fadeRate = 5f;
	float chargeTimer = 0;
	int energy = 3;
	
	bool superCharged = false;
	bool shieldDead = false;
	// Use this for initialization
	void Start () {
		shield = max;
	}
	
	// Update is called once per frame
	void Update () {
		
		timer+= Time.deltaTime;
		
		if (superCharged) {
			chargeTimer+= Time.deltaTime;
		}
		if (!superCharged) {
			fade += fadeRate * Time.deltaTime;
			if (timer > 0.1f) {
				if (fade > 500f) {
					fade = 1000000;
				}
				gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f/ fade);
				
			}
		}
		
		if (timer > regenCD) {
			shield += regenAmount * Time.deltaTime;
			//dead, wait 5s, restart shield at 50
			if (timer >= 5 && shieldDead) {
				shield = 50;
				renderer.enabled = true;
				collider2D.enabled = true;
			}
		}
		
		if (shield > max) {
			shield = max;
		}
		
		//end super charge
		if (chargeTimer > 2f) {
			superCharged = false;
			chargeTimer = 0;
		}
		if (Input.GetKeyDown(KeyCode.Space) && !superCharged && energy > 0) {
			superShield();
			energy--;
		}
		shieldHUD.text = "shield " + (int)shield;
		energyHUD.text = "energy " + energy;
		
	}
	
	void OnCollisionEnter2D( Collision2D other) {
		if (!superCharged) {
			timer = 0;
			shield -= other.gameObject.GetComponent<enemylaser>().damage;
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.8f * shield/max);
			fade = 1f;
		}
		Instantiate(hitExplosion, other.contacts[0].point, transform.rotation);
		audio.PlayOneShot(sounds[0]);
		if (shield <= 0) {
			shieldDown();
		
		}

	
	}
	
	public void shieldDown() {
		shieldDead = true;
		renderer.enabled =false;
		collider2D.enabled =false;
	}
	
	void superShield() {
		shield = 100;
		superCharged = true;
		shieldDead = false;
		renderer.enabled =true;
		collider2D.enabled =true;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
		
		
	}
}
