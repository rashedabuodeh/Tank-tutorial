using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class canvas : MonoBehaviour
{
    public Text scoreText1;
    public Text scoreText2;
    public int score1 = 0;
    public int score2 = 0;



    void Start()
    {
        scoreText1 = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        scoreText2 = GameObject.FindGameObjectWithTag("ScoreText2").GetComponent<Text>();

        scoreText1.text = ":"+ score1;
        scoreText2.text = ":" + score2;



    }
    public void pause()
    {
        SceneManager.LoadScene("pause", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    public void Update()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene0"));

        if (!GameObject.FindGameObjectWithTag("tank2"))
            {
                score1 += 1;
                scoreText1.text = ":" + score1;
                SceneManager.UnloadSceneAsync("scene0");
                SceneManager.LoadScene("scene0", LoadSceneMode.Additive);


        }

        if (!GameObject.FindGameObjectWithTag("tank1"))
        {
            score2 += 1;
            scoreText2.text = ":" + score2;
            SceneManager.UnloadSceneAsync("scene0");
            SceneManager.LoadScene("scene0", LoadSceneMode.Additive);
        }

    }
}
