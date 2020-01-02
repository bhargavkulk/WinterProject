using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : MonoBehaviour {
    public Transform player;
    public float speed = 1f;
    public float fireRate = 2f;
    public float movementInterval = 2f;
    public float timeToNextShot;
    public Bullet bulletPrefab;

    private Vector2 direction;
    private bool isMoving;
    private float nextToggle;

    private void Start() {
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
        } else {
            Attack();
        }
        Rotate();
        Debug.DrawLine(transform.position, player.position, Color.green);
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
}
