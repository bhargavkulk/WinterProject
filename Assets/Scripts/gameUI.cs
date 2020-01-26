using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour
{

    public Text scoreText;
    float timer;
    public static float score = 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        score = (Bullet.enemy1count + PlayerManager.count2)*20 + timer*2;
        scoreText.text = score.ToString("0");
    }
}
