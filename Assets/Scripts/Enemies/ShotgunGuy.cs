using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunGuy : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;
    public float fireRate = 1f;
    public float movementInterval = 2f;
    public float timeToNextShot;
    public float bulletSpread = 1f;
    public Bullet bulletPrefab;

    private Vector2 direction;
    private Vector3 dir;
    private Vector2 directionL1;
    private Vector2 directionL2;
    private Vector2 directionR1;
    private Vector2 directionR2;
    private bool isMoving;
    private float nextToggle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nextToggle = Time.time;
        isMoving = true;
        timeToNextShot = 1f / fireRate; 
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        FindDirection();
        Toggle();

        if(isMoving) {
            Move();
        } else {
            Attack();
        }
        Rotate();
        Debug.DrawLine(transform.position, player.position, Color.green);
        
    }

    private void FindDirection(){
        direction = (player.position - transform.position).normalized;

        dir.x=player.position.x+bulletSpread;
        dir.y=player.position.y+bulletSpread;
        dir.z=0;
        directionL1 =(dir-transform.position).normalized;

        dir.x=player.position.x+(2*bulletSpread);
        dir.y=player.position.y+(2*bulletSpread);
        dir.z=0;
        directionL2 =(dir-transform.position).normalized;

        dir.x=player.position.x-bulletSpread;
        dir.y=player.position.y-bulletSpread;
        dir.z=0;
        directionR1 =(dir-transform.position).normalized;

        dir.x=player.position.x-(2*bulletSpread);
        dir.y=player.position.y-(2*bulletSpread);
        dir.z=0;
        directionR2 =(dir-transform.position).normalized;   
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
            Bullet bulletL1 = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);
            bulletL1.speed = 5f;
            bulletL1.direction = directionL1;
            Bullet bulletL2 = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);
            bulletL2.speed = 5f;
            bulletL2.direction = directionL2;
            Bullet bulletR1 = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);
            bulletR1.speed = 5f;
            bulletR1.direction = directionR1;
            Bullet bulletR2 = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);
            bulletR2.speed = 5f;
            bulletR2.direction = directionR2;
        
        }
    }
}
