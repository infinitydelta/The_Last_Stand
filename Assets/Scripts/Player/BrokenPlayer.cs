using UnityEngine;
using System.Collections;

public class BrokenPlayer : MonoBehaviour {
    Vector3 vel;
	// Use this for initialization
	void Start () {
        vel = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(vel * Time.deltaTime);
        vel *= .9999f;
	}
}
