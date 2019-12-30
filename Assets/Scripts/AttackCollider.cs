using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

    private PlayerManager playerManager;
    public List<Collider2D> TriggerList = new List<Collider2D>();

    void Start() {
        playerManager = transform.parent.GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        TriggerList.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        TriggerList.Remove(other);
    }
}
