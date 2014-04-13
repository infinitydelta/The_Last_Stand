using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
	
	float negX;
	// Use this for initialization
	void Start () {
		negX = Random.Range(-1.5f, 2.5f);
		if (negX> 0) {
			negX = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(negX* Time.deltaTime, 0, 0, Space.World);
		
		if (transform.position.x <= Camera.main.ViewportToWorldPoint(Vector3.zero).x) {
			float y = Random.Range(0f, 1f);
			transform.position = (Camera.main.ViewportToWorldPoint(new Vector3(1, y, 10) ));
		}
	}
}
