using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour {
	float initialIntensity;
	public float lowerIntensity = 0f;
	float flickerCounter = 0f;
	public float flickerRate = 0.01f;
	// Use this for initialization
	void Start () {
		initialIntensity = light.intensity;
		flickerCounter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = lowerIntensity + (initialIntensity - lowerIntensity) * (Mathf.Cos (flickerCounter) / 2f + 0.5f);
		flickerCounter += flickerRate;
	}
}
