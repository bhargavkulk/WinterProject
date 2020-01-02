using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "bullet") {
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "bullet") {
            Destroy(other.gameObject);
        }
    }
}
