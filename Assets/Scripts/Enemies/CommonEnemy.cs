using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : MonoBehaviour {
    public Transform player;
    public float speed = 1f;
    public float fireRate = 1f;
    public float movementInterval = 2f;
    public float timeToNextShot;
    public Bullet bulletPrefab;

    private Vector2 direction;
    private bool isMoving;
    private float nextToggle;
    private float xLimit;
    private float yLimit;


    private void Start() {
        xLimit = Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x / 2f;
        yLimit = Camera.main.orthographicSize - transform.localScale.y / 2f;

        player = GameObject.FindWithTag("Player").transform;
        nextToggle = Time.time;
        isMoving = true;
        timeToNextShot = 1f / fireRate; 
    }

    private void FixedUpdate() {

        direction = (player.position - transform.position).normalized;
        Toggle();

        if(isMoving) {
            Move();
        } else if(IsInScreen()) {
            Attack();
        }
        Rotate();
    }
    
    private void Move() {
        transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
    }

    private void Rotate() {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Toggle() {
        if(Time.time > nextToggle) {
            isMoving = !isMoving;
            nextToggle = Time.time + movementInterval;
        }
    }

    private void Attack() {
        timeToNextShot -= Time.fixedDeltaTime;

        if(timeToNextShot <= 0) {
            timeToNextShot = 1f / fireRate;
            Bullet bullet = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);
            bullet.speed = 5f;
            bullet.direction = direction;
        }
    }

    private bool IsInScreen() {
        if(Mathf.Abs(transform.position.x) < xLimit) {
            if(Mathf.Abs(transform.position.y) < yLimit) {
                return true;
            }
            return false;
        }
        return false;
    }
}
