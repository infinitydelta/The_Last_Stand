using UnityEngine;
using System.Collections;

public class EndExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<CircleCollider2D>().radius += (Time.deltaTime * 20);
       
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<Enemy>().endGameDeath();
        }
    }
}
