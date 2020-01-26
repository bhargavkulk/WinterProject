using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector2 direction = Vector2.zero;
    public float speed = 0;
    public bool isDeflected;
    public static float enemy1count = 0f;
    public HealthSpawn healthPack;
  
    void Start() {
        isDeflected = false;    
    }

    void FixedUpdate() {
        transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "enemy" && isDeflected) {
            FindObjectOfType<AudioManager>().Play("Kill");
            Destroy(other.gameObject);
            enemy1count = enemy1count + 1;
        }
        if(other.gameObject.tag == "shotgunguy" && isDeflected) {
            FindObjectOfType<AudioManager>().Play("Kill");
            Instantiate(healthPack, (Vector2)other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    
}
