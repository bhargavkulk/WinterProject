using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exitmenu : MonoBehaviour
{
    private void Start() {
        Cursor.visible = true;
    }

    public Text final;
    void FixedUpdate() {
        final.text = gameUI.score.ToString("0");
    }

    public void retry() {
        Cursor.visible = false;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame() {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.Quit();
    }
}
