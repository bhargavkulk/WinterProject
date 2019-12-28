using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Vector2 xRange;
    private Vector2 yRange;
    private Vector2 input;
    private Vector2 velocity;
    private Vector2 mousePos;
    private bool isDashing;
    private Vector2 dashDir;

    public float movementSpeed = 4f;
    public float dampingCoeff = 2f;
    public float maxSpeed = 16f;
    public float dashFactor = 0.5f;

    void Start() {
        float xLimit = Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x / 2f;
        float yLimit = Camera.main.orthographicSize - transform.localScale.y / 2f;

        xRange = new Vector2(-xLimit, xLimit);
        yRange = new Vector2(-yLimit, yLimit);

        velocity = Vector2.zero;
        isDashing = false;
    }

    void Update() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate() { 
        if(Input.GetMouseButtonDown(0)) {
            isDashing = true;
            dashDir = ((Vector2)mousePos - (Vector2)transform.position).normalized;
        }

        if(!isDashing) {
            if(input == Vector2.zero) {
                SlowDown();
            } else {
                SpeedUp();
            }

            Move();
            RotateWithMouse();
        } else {
            Dash(dashDir);
        }
    }

    private void SpeedUp() {
        velocity += input * movementSpeed;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
    }

    private void SlowDown() {
        velocity = Vector2.zero;
    }

    private void Move() {
        transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, xRange.x, xRange.y),
            Mathf.Clamp(transform.position.y, yRange.x, yRange.y)
        );
    }

    private void RotateWithMouse() {
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, 
            mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Dash(Vector2 dashDir) {
        Vector2 newPos = (Vector2)transform.position + dashFactor * dashDir;
        transform.position = newPos;
        isDashing = false;
    }
}
