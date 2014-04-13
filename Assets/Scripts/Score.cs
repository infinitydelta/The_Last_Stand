using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	int score = 0;

	public GUIText scoreText;
	public AudioClip[] music;
	// Use this for initialization
	void Start () {
		int i = Random.Range (0, 3);
		print (i);
		audio.PlayOneShot (music [i]);
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "SCORE\n" + score;
	}
	
	public void addScore(int i) {
		score += i;
	}
}
