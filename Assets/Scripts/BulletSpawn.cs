using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    public float minDist;
    public GameObject g;
    public GameObject BulletPrefab;
    private Vector2 dir;
    public Transform player;
    public float speed = 4f;
    public float ShotInterval;
    private float timer;
    public Vector2 movement;
    public Rigidbody2D rb;
    public float TravelTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= ShotInterval)
        {
         
            timer = 0f;
            dir = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
            g = Instantiate(BulletPrefab, (Vector2)this.transform.position + dir.normalized, Quaternion.identity);
            rb = g.GetComponent<Rigidbody2D>();
        }

        movement = dir;

        moveBullet(movement);

    }

    void moveBullet(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(GameObject.FindGameObjectWithTag("bullet"));
    }
}
