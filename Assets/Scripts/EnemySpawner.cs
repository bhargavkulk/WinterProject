using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject shotgunGuyPrefab;
    private float timer;
    public float SpawnInterval = 5f;
    public float inRadius = 1;
    public float outRadius = 2;
    public int spawnCount = 0;
    public int shotgunnerSpawnTime = 2;
    
    void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer>=SpawnInterval) {
            timer = 0f;
            Vector2 pos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            pos = pos.normalized;
            pos *= Random.Range(inRadius, outRadius);

            GameObject enemy;

            spawnCount += 1;
            if(spawnCount % shotgunnerSpawnTime == 0) {
                enemy = Instantiate(shotgunGuyPrefab, pos, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
            } else {
                enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
            }
        }
    }
}
