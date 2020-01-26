using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public CommonEnemy enemyPrefab;
    public ShotgunGuy shotgunGuyPrefab;
    private float enemySpeed;
    private float rateOfFire;
    private float timer;
    private float globalTimer;
    private float startTime;
    public float spawnInterval = 5f;
    public float waveChangeInterval = 10f;
    public float inRadius = 1;
    public float outRadius = 2;
    public int spawnCount = 0;
    public int shotgunnerSpawnTime = 2;
    
    void Start() {
        startTime = Time.time;
        enemySpeed = 1f;
        rateOfFire = 1f;
        timer = 0;
        globalTimer = 0;
    }
    
    void FixedUpdate() {
        timer += Time.deltaTime;
        globalTimer += Time.deltaTime;
        if(timer >= spawnInterval) {
            timer = 0f;
            Vector2 pos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            pos = pos.normalized;
            pos *= Random.Range(inRadius, outRadius);

            spawnCount += 1;
            if(spawnCount % shotgunnerSpawnTime == 0) {
                ShotgunGuy enemy = Instantiate(shotgunGuyPrefab, pos, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
                enemy.speed = enemySpeed;
            } else {
                CommonEnemy enemy = Instantiate(enemyPrefab, pos, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
                enemy.speed = enemySpeed;
            }
        }

        if(globalTimer >= waveChangeInterval) {
            globalTimer = 0f;
            enemySpeed += 1f;
            rateOfFire += 0.25f;
            spawnInterval -= 1f;
            if(spawnInterval < 1f) {
                spawnInterval = 1f;
            }
        }
    }
}
