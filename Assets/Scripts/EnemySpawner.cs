using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] enemies;
	
	float timer = 0;
	float spawnCooldown = 1f;
	int spawned;
	int increaseIndex = 50;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= spawnCooldown) {
			spawnCommonEnemy();
			timer = 0;
		}
		if (spawned >= increaseIndex) {
			spawnCooldown *= .8f;
			increaseIndex *= 2;
		}
		
	}
	
	void spawnCommonEnemy() {
		spawned++;
		int index = Random.Range(0,enemies.Length);
//		Vector3 v3Pos = new Vector3(-0.15f, .5f, 10);
//		v3Pos = Camera.main.ViewportToWroldPoint(v3Pos);
		Instantiate(enemies[index], randomEnemySpawn(), Quaternion.identity);
	}
	
//	void spawnEnemy() {
//		if (Random.Range(0,2) == 1) {
//			Instantiate (enemy1, randomEnemySpawn() , Quaternion.identity);
//		}
//		else {
//			Instantiate (enemy2, randomEnemySpawn() , Quaternion.identity);
//		}
//	}
	
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
