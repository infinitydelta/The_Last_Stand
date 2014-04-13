using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "GAME OVER\n CONTINUE?";

		if (Input.anyKey) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
