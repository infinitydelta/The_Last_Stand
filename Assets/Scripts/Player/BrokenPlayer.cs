using UnityEngine;
using System.Collections;

public class BrokenPlayer : MonoBehaviour {
    public Vector3 vel;
	// Use this for initialization
	void Start () {
        //if no predetermined movement in x
        if (vel.x == 0)
        {
            vel = new Vector3(Random.Range(-1f, 1f), vel.y, 0);
        }
        vel *= Random.Range(.1f, .5f);
        //vel = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(vel * Time.deltaTime);
        vel *= .9999f;
	}
}
