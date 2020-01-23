using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private Vector2 xRange;
    private Vector2 yRange;
    private Vector2 input;
    private Vector2 velocity;
    private Vector2 mousePos;
    private bool isDashing;
    private Vector2 dashDir;
    private AttackCollider attackCollider;
    private Vector2 lookDir;

    public float movementSpeed = 4f;
    public float dampingCoeff = 2f;
    public float maxSpeed = 16f;
    public float dashFactor = 0.5f;
    public float health = 100f;
    public float bulletHealthDecrease = 5f;
    public SimpleHealthBar healthBar;

    void Start() {
        float xLimit = Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x / 2f;
        float yLimit = Camera.main.orthographicSize - transform.localScale.y / 2f;

        xRange = new Vector2(-xLimit, xLimit);
        yRange = new Vector2(-yLimit, yLimit);

        velocity = Vector2.zero;
        isDashing = false;
        attackCollider = transform.GetChild(0).gameObject.GetComponent<AttackCollider>();
    }

    void Update() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - (Vector2)transform.position;
    }

    private void FixedUpdate() { 
        if(Input.GetKeyDown(KeyCode.Space)) {
            isDashing = true;
            dashDir = ((Vector2)mousePos - (Vector2)transform.position).normalized;
        }

        if(isDashing) {
            Dash(dashDir);
        } else {
            if(Input.GetMouseButtonDown(0)) {
                Attack();
        }
            if(input == Vector2.zero) {
                SlowDown();
            } else {
                SpeedUp();
            }

            Move();
            RotateWithMouse();
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
        FindObjectOfType<AudioManager>().Play("Dash");
    }

    private void Attack() {
        var currTriggerList = attackCollider.TriggerList;
        foreach (var other in attackCollider.TriggerList) {
            if(other.gameObject.tag == "enemy") {
                Destroy(other.gameObject);
                FindObjectOfType<AudioManager>().Play("Kill");
            } else if(other.gameObject.tag == "bullet") {
                health+= bulletHealthDecrease;
                healthBar.UpdateBar(health, 100f);
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                bullet.direction = lookDir.normalized;
                bullet.isDeflected = true;
                FindObjectOfType<AudioManager>().Play("Deflect");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "bullet"){
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if(bullet.isDeflected == false)
            {
                health-= bulletHealthDecrease;
                healthBar.UpdateBar(health, 100f);
                if(health == 0f)
                {
                    GameOver();
                }
            }
        }
    }
    private void GameOver(){
        FindObjectOfType<AudioManager>().Play("Died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator AttackTime() {
        yield return new WaitForSeconds(1f);
    }
}
