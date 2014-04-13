using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
	float timer;
	bool canShoot = false;
	float cooldown;
	
	public GameObject laser;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		cooldown = Random.Range(7f, 14f);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= cooldown) {
			canShoot = true;
			//recoilRecovery();
		}
		else {
			timer += Time.deltaTime;
			canShoot = false;
			
		}
		shoot ();
	}
	
	
	void shoot() {
		if (canShoot && transform.parent.gameObject.renderer.isVisible) {
			//Vector3 moz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//print (moz);
			Vector2 dir = (player.transform.position - transform.position);
			//print (dir);
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			Instantiate (laser, new Vector3(transform.position.x,transform.position.y, transform.position.z +1) , Quaternion.Euler(0,0,angle) );
			cooldown = Random.Range(10f, 20f);
			canShoot = false;
			timer = 0;
		}
	}
}
