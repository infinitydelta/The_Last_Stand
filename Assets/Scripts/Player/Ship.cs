using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject laser;
	public AudioClip[] gunsound ;
	
	float timer = 0;
	bool canShoot;
	float stoppedShootingTime = 0;
	float cooldown = .1f;
	
	float rotationSpeed = 5;
	// Use this for initialization
	void Start () {
		canShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
		faceDirection();
		if (timer >= .05f) {
			light.enabled = false;
		}
	
		if (timer >= cooldown) {
			canShoot = true;
			//recoilRecovery();
		}
		else {
			timer += Time.deltaTime;
			canShoot = false;
			stoppedShootingTime = 0;
		}
		
		if (Input.GetMouseButton(0)) {
			shoot ();
			//Camera.main.transform.Translate(Random.insideUnitCircle* 5 * Time.deltaTime,0);
		}
	}
	
	
	void shoot() {
		
		if (canShoot) {
			Vector3 moz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 dir = (moz - transform.position);
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			//shoot slightly infront of gun
			float r = 1f;
			Instantiate(laser, new Vector3(transform.position.x + r*Mathf.Cos((transform.rotation.eulerAngles.z)* Mathf.Deg2Rad) ,r*Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad )+ transform.position.y, transform.position.z) , transform.rotation/*Quaternion.Euler(0,0,angle)*/ );
			
			canShoot = false;
			light.enabled = true;
			//Camera.main.transform.Translate(Random.insideUnitCircle, 0);
			Camera.main.GetComponent<CameraShake>().shakeAmount = .05f;
			Camera.main.GetComponent<CameraShake>().shake = .1f;
			int i = Random.Range(0, 3);
			audio.PlayOneShot(gunsound[0]);
			timer = 0;
		}
	}
	
	void faceDirection() {	
		
		//aim at mouse
		Vector3 moz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 hurr = new Vector3( moz.x, moz.y, 0);
		float angle = Mathf.Atan2(hurr.y - transform.position.y, hurr.x - transform.position.x) * Mathf.Rad2Deg;
		//print (hurr);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,angle), rotationSpeed);
		
		
	}
}
