using UnityEngine;

public class CursorManager : MonoBehaviour {

    void Start() {
        Cursor.visible = false;
    }

    void FixedUpdate() {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPosition;
    }
}
