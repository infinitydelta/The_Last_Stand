using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {
	public int speed = 20;
	
	Vector2 vel;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 3);
		vel = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed, speed * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
		rigidbody2D.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		//print ("hi");
		if (other.gameObject.CompareTag("enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(20);
		}
		Destroy(this.gameObject);
	}
}
