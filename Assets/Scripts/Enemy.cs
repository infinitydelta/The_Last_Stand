using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	int hp=100;
	float rotationSpeed = 3f;
	float orbitSpeed = 0;
	float orbit;
	float speed;
	int maxDis = 35;
    Vector3 prevPos;
    Vector3 currPos;
    double oldx, oldy;
    double newx, newy;
    float deltax, deltay;
	
	public GameObject broken;
	public GameObject explosion;
	public GameObject whiteexplosion;
	public GameObject explosionlong;
	
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		orbit = Random.Range(2f, 10f);
		//orbitSpeed = Random.Range(-.3f, .3f );
		while (orbitSpeed == 0) {
			orbitSpeed = Random.Range(-10f, 10f );
		}
		speed = Random.Range(100f, 200f);
		//rigidbody2D.AddForce((player.transform.position - transform.position).normalized * speed * 50);
        prevPos = transform.position;
        currPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float fps = 1f / Time.deltaTime;
        //prevPos = currPos;

		//if spinning out of control
		if (rigidbody2D.angularVelocity > 5) {
			transform.Translate(-1 * Time.deltaTime, 0, 0, Space.World);
		}
		else {
			faceDirection(player);
			
		}
		//orbit around player
		//move towards player if outside of orbit range
		if (Vector2.Distance(player.transform.position, transform.position) > orbit) {
			rigidbody2D.AddForce((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
		}
		else {
            //rigidbody2D.velocity = ( (Vector2)(rigidbody2D.velocity) - (Vector2)((player.transform.position - transform.position).normalized) );
			rigidbody2D.velocity *= .9f;
			//rigidbody2D.AddForce(-(player.transform.position - transform.position).normalized * speed );
		}
        //faster if closer
		transform.RotateAround(player.transform.position, new Vector3(0,0,1), Time.deltaTime * (orbitSpeed + Mathf.Sign(orbitSpeed) * .3f/Vector2.Distance(player.transform.position, transform.position)) ); //orbit around player
        //currPos = transform.position;
        
        newx = transform.position.x;
        newy = transform.position.y;
        oldx = prevPos.x;
        oldy = prevPos.y;
        deltax = (float)(newx - oldx);
        deltay = (float)(newy - oldy);
        prevPos = transform.position;
		if (Vector2.Distance(transform.position, player.transform.position) > maxDis) {
			Destroy(gameObject);
		}
        //print("prev: " + prevPos);
        //print("curr: " + currPos);
        //print("oldx: " + oldx + ", oldy: " + oldy );
        //print("newx: " + newx + ", newy: " + newy);

        //print(deltax  + ", " + deltay);

	}
	
	public void takeDamage(int damage) {
		hp -= damage;

		
		if (hp <= 0) {
			death();
		
		}
	}

    public void endGameDeath()
    {
        GameObject pieces = (GameObject)Instantiate(broken, transform.position, transform.rotation);
        pieces.rigidbody2D.angularDrag = 1;
        pieces.rigidbody2D.angularVelocity = rigidbody2D.angularVelocity;
        pieces.GetComponent<BrokenWhole>().playExplosion = false;


        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.particleSystem != null)
            {
                child.particleSystem.loop = false;
                child.transform.parent = pieces.gameObject.transform;
            }
        }

        Destroy(this.gameObject);
    }
	
	void death() {
		GameObject pieces = (GameObject) Instantiate(broken, transform.position, transform.rotation);
        pieces.GetComponent<BrokenWhole>().oldx = deltax / Time.deltaTime;
        pieces.GetComponent<BrokenWhole>().oldy = deltay/ Time.deltaTime;

        pieces.rigidbody2D.angularDrag = 1;
		pieces.rigidbody2D.angularVelocity = rigidbody2D.angularVelocity ;
		
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.particleSystem != null) {
				child.particleSystem.loop = false;
				child.transform.parent = pieces.gameObject.transform;
			}
		}
		
		Destroy(this.gameObject);
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag("projectiles")) {
			Instantiate(explosion, other.contacts[0].point, transform.rotation);
		
			if (hp <= 40) {
				GameObject leak = (GameObject) Instantiate(explosionlong, other.contacts[0].point, transform.rotation);
				leak.transform.parent = gameObject.transform;
                //Destroy(gameObject, 2);
                Invoke("death", 4);
			}
		}

		if (other.gameObject.CompareTag("autoprojectiles")) {
			Instantiate(whiteexplosion, other.contacts[0].point, transform.rotation);
			
			if (hp <= 40) {
				GameObject leak = (GameObject) Instantiate(explosionlong, other.contacts[0].point, transform.rotation);
				leak.transform.parent = gameObject.transform;
                Invoke("death", 4);
			}
		}
		
		if (other.gameObject.CompareTag("Player")) {
			death ();
		}
	}
	
	void faceDirection(GameObject target) {		
		//aim in direction of movement
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, direction * 45), rotationSpeed);
		
		Vector3 hurr = new Vector3( target.transform.position.x, target.transform.position.y, 0);
		float angle = Mathf.Atan2(hurr.y - transform.position.y, hurr.x - transform.position.x) * Mathf.Rad2Deg;
		//print (hurr);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,angle), rotationSpeed);
		
		
	}
}
