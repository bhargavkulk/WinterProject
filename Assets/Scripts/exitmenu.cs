using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exitmenu : MonoBehaviour
{

    public Text final;
    void FixedUpdate()
    {
        final.text = gameUI.score.ToString("0");
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
