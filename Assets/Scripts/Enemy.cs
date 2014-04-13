﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	int hp=100;
	float rotationSpeed = 3f;
	float orbitSpeed = 0;
	float orbit;
	float speed;
	int maxDis = 35;
	
	public GameObject broken;
	public GameObject explosion;
	public GameObject whiteexplosion;
	public GameObject explosionlong;
	
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		orbit = Random.Range(2f, 10f);
		//orbitSpeed = Random.Range(-.3f, .3f );
		while (orbitSpeed == 0) {
			orbitSpeed = Random.Range(-10f, 10f );
		}
		speed = Random.Range(100f, 200f);
		//rigidbody2D.AddForce((player.transform.position - transform.position).normalized * speed * 50);
		
	}
	
	// Update is called once per frame
	void Update () {
		//if spinning out of control
		if (rigidbody2D.angularVelocity > 5) {
			transform.Translate(-1 * Time.deltaTime, 0, 0, Space.World);
		}
		else {
			faceDirection(player);
			
		}
		//orbit player
		
		if (Vector2.Distance(player.transform.position, transform.position) > orbit) {
			rigidbody2D.AddForce((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
		}
		else {
			rigidbody2D.velocity *= .98f;
			//rigidbody2D.AddForce(-(player.transform.position - transform.position).normalized * speed );
		}
		transform.RotateAround(player.transform.position, new Vector3(0,0,1), Time.deltaTime * (orbitSpeed + Mathf.Sign(orbitSpeed) * .3f/Vector2.Distance(player.transform.position, transform.position)) ); //orbit around player
		
		if (Vector2.Distance(transform.position, player.transform.position) > maxDis) {
			Destroy(gameObject);
		}

	}
	
	public void takeDamage(int damage) {
		hp -= damage;

		
		if (hp <= 0) {
			death();
		
		}
	}
	
	void death() {
		GameObject pieces = (GameObject) Instantiate(broken, transform.position, transform.rotation);
		pieces.rigidbody2D.angularDrag = 1;
		pieces.rigidbody2D.angularVelocity = rigidbody2D.angularVelocity ;
		
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.particleSystem != null) {
				child.particleSystem.loop = false;
				child.transform.parent = pieces.gameObject.transform;
			}
		}
		
		Destroy(this.gameObject);
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag("projectiles")) {
			Instantiate(explosion, other.contacts[0].point, transform.rotation);
		
			if (hp <= 40) {
				GameObject leak = (GameObject) Instantiate(explosionlong, other.contacts[0].point, transform.rotation);
				leak.transform.parent = gameObject.transform;
			}
		}

		if (other.gameObject.CompareTag("autoprojectiles")) {
			Instantiate(whiteexplosion, other.contacts[0].point, transform.rotation);
			
			if (hp <= 40) {
				GameObject leak = (GameObject) Instantiate(explosionlong, other.contacts[0].point, transform.rotation);
				leak.transform.parent = gameObject.transform;
			}
		}
		
		if (other.gameObject.CompareTag("Player")) {
			death ();
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
}