using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if ((int) (timer*20 % 2) == 0) {
			renderer.enabled = true;
		}
		else {
			renderer.enabled = false;
		}
		
		timer = timer % 100000;
	}
}
