using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour {
	public GameObject star;
	int size = 100;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < size; i++) {
			Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f,1f) , Random.Range(0f,1f), 20));
			GameObject newStar = (GameObject) Instantiate(star, pos, Quaternion.identity);
			float scale = Random.Range(.1f, 1.3f);
			newStar.transform.localScale = new Vector3(scale, scale, scale);
			newStar.light.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			newStar.light.range = Random.Range(.1f, .4f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
