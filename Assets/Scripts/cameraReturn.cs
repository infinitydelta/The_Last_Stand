using UnityEngine;
using System.Collections;

public class cameraReturn : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( new Vector3((player.transform.position - transform.position).x, (player.transform.position - transform.position).y, 0) * Time.deltaTime);
	}
}
