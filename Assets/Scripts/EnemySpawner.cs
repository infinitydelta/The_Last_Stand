using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] enemies;
    public GameObject player;
    bool playerDead = false;

	float timer = 0;
	float spawnCooldown = 1.5f;
    bool spawn = false;
    int startTimer = 5;
	int spawned = 0;
	int increaseIndex = 50;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("hull");
        //player = Transform.FindChild("hull");
        playerDead = player.GetComponent<Hull>().dead;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
        
        //start delay
        if (timer >= startTimer) spawn = true;

		if (spawn && timer >= spawnCooldown) {
			spawnCommonEnemy();
			timer = 0;
		}
		if (spawned >= increaseIndex) {
			spawnCooldown *= .8f;
			increaseIndex += 50;
		}
        if (player.GetComponent<Hull>().dead)
        {
            Destroy(gameObject);
        }
		
	}
	
	void spawnCommonEnemy() {
		spawned++;
		int index = Random.Range(0,enemies.Length);
//		Vector3 v3Pos = new Vector3(-0.15f, .5f, 10);
//		v3Pos = Camera.main.ViewportToWroldPoint(v3Pos);
		Instantiate(enemies[index], randomEnemySpawn(), Quaternion.identity);
	}
	
	

    //spawn enemy offscreen
	Vector3 randomEnemySpawn() {
		float offset = .03f;
		Vector3 spawnPoint = new Vector3(0,0,10);
		if (Random.Range (0, 2) == 1) { //offscreen up/down
			
			spawnPoint.x = Random.Range (0, 1f); //xscreen pos
			
			if (Random.Range (0, 2)== 1) {
				spawnPoint.y = 1 + offset;
			} else {
				spawnPoint.y = 0 - offset;
			}
			
		} else {
			spawnPoint.y = Random.Range (0, 1f);
			
			if (Random.Range (0, 2) == 1) {
				spawnPoint.x = 1 + offset;
			} else {
				spawnPoint.x = 0 - offset;
			}
			
		}
		spawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
		return spawnPoint;
	}
		
}
