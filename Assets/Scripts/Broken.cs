using UnityEngine;
using System.Collections;

public class Broken : MonoBehaviour {
	Vector3 vel;
	Vector3 rot;
	float negX = -2f;
	// Use this for initialization
	void Start () {
		vel = new Vector3(Random.Range(-.5f,.5f), Random.Range(-.5f,.5f), 0);
		rot = new Vector3(0,0, Random.Range(-5f,5f));
		Destroy(this.gameObject, Random.Range(5, 10));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(/*(new Vector3(negX,0,0) +*/ vel * Time.deltaTime);
		transform.Rotate(rot * Time.deltaTime);
	}
}
