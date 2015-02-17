using UnityEngine;
using System.Collections;

public class AutoGun : MonoBehaviour {
	float timer = 0;
	bool canShoot = false;
	float stoppedShootingTime = 0;
	float cooldown = .15f;
	
	GameObject target;
	float rotationSpeed = 100;
	
	public GameObject laser;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		
		if (timer >= cooldown) {
			canShoot = true;
		}
		else {
			timer += Time.deltaTime;
			canShoot = false;
			stoppedShootingTime = 0;
		}
		if (target != null) {
			shoot (target);
			if (Vector2.Distance( target.transform.position, transform.position) > 20) {
				target = null;
			}
			//faceDirection(target);
			
			//print ("firing");
		}

	}
	
	
	void faceDirection(GameObject target) {		
		//aim in direction of movement
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, direction * 45), rotationSpeed);
		
		Vector3 hurr = new Vector3( target.transform.position.x, target.transform.position.y, 0);
		float angle = Mathf.Atan2(hurr.y - transform.position.y, hurr.x - transform.position.x) * Mathf.Rad2Deg;
		//print (hurr);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,angle), rotationSpeed);
		
		
	}
	
	void OnTriggerEnter2D( Collider2D other) {
		//print (other.gameObject);
		if (other.gameObject.CompareTag("enemy")) {
			if (target == null) {
				target = other.gameObject;
			}
		}
	}
	void OnTriggerStay2D( Collider2D other) {
		//print (other.gameObject);
		if (other.gameObject.CompareTag("enemy")) {
			if (target == null) {
				target = other.gameObject;
			}
		}
	}
	
	void OnTriggerExit2D ( Collider2D other){
		if (target == other.gameObject) {
			target = null;
		}
	}
	
	
	void shoot(GameObject target) {
		if (canShoot) {
			//Vector3 moz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//print (moz);
			Vector2 dir = target.transform.position  - transform.position;
			//print (dir);
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			Instantiate (laser, new Vector3(transform.position.x,transform.position.y, transform.position.z +1) , Quaternion.Euler(0,0,angle) );
			//cooldown = Random.Range(10f, 15f);
			canShoot = false;
			
			//audio.PlayOneShot(sound);
			timer = 0;
		}
	}
}
