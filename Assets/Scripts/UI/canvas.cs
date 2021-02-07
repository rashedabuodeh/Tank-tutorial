using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class canvas : MonoBehaviour
{
    public Text scoreText1;
    public Text scoreText2;
    public Text lvlText;
    public Text winText;

    public int score1 = 0;
    public int score2 = 0;
    int lvl = 1;
    public GameObject WIN;



    void Start()
    {
        scoreText1 = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        scoreText2 = GameObject.FindGameObjectWithTag("ScoreText2").GetComponent<Text>();
        lvlText = GameObject.FindGameObjectWithTag("lvl").GetComponent<Text>();
        winText = GameObject.FindGameObjectWithTag("win").GetComponent<Text>();

        lvlText.text = "Level" + lvl;
        scoreText1.text = ":"+ score1;
        scoreText2.text = ":" + score2;



    }

    // Update is called once per frame
    public void Update()
    {
       // if (SceneManager.GetSceneByName("gamescene")!=null)
        if (SceneManager.GetActiveScene().name != "gamescene")
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("gamescene"));
            } 
        

          if (!GameObject.FindGameObjectWithTag("tank2") && score1 < 3)
          {
                lvl += 1;
                score1 += 1;
                scoreText1.text = ":" + score1;
          
                lvlText.text = "Level " + lvl;
                SceneManager.UnloadSceneAsync("gamescene");
                SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);
           
          }

        if (!GameObject.FindGameObjectWithTag("tank1")&& score2<3)
        {
            lvl += 1;
            score2 += 1;
            scoreText2.text = ":" + score2;
            
                lvlText.text = "Level " + lvl;
                SceneManager.UnloadSceneAsync("gamescene");
                SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);
            
        }

        if (score2 >=3 && !GameObject.FindGameObjectWithTag("winner"))
        {
            lvl -=1 ;
            lvlText.text = "Level " + lvl;

            winText.text = "Player 2 won " ;
            Instantiate(WIN,transform);
        }
        if (score1 >= 3 && !GameObject.FindGameObjectWithTag("winner"))
        {
            lvl -= 1;
            lvlText.text = "Level " + lvl;

            winText.text = "Player 1 won " ;
            Instantiate(WIN, transform);

        }


    }


}
