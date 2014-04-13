using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	float timer;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 4);
		guiText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= 1f) {
			guiText.enabled = true;

		}
	}
}
