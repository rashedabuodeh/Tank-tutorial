using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class canvas_online : MonoBehaviourPun, IPunObservable
{
    private PhotonView PV;
    public Text scoreText1;
    public Text scoreText2;
    public Text lvlText;
    public Text winText;

    public int score1 = 0 , score11=0, score22=0;
    public int score2 = 0;
    public int lvl = 1, lvll = 1;
    public GameObject WIN;

    bool flag1, flag2 ;

    public GameObject playerPrefab;
    public GameObject playerPrefab2;






    void Start()
    {
       // PV = GetComponent<PhotonView>();
        scoreText1 = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        scoreText2 = GameObject.FindGameObjectWithTag("ScoreText2").GetComponent<Text>();
        lvlText = GameObject.FindGameObjectWithTag("lvl").GetComponent<Text>();
        winText = GameObject.FindGameObjectWithTag("win").GetComponent<Text>();

        lvlText.text = "Level" + lvl;
        scoreText1.text = ":" + score1;
        scoreText2.text = ":" + score2;



    }

    // Update is called once per frame
    public void Update()
    {



        if (GameObject.FindGameObjectWithTag("tank2")!=null)
        {

            flag2 = true;
        }
        if (GameObject.FindGameObjectWithTag("tank1")!=null )
        {
            flag1 = true;


        }


        if (photonView.IsMine)
        {

            if (GameObject.FindGameObjectWithTag("tank2") == null && score1 < 3 && flag2)
            {
                lvl += 1;
                score1 += 1;
                scoreText1.text = ":" + score1;

                lvlText.text = "Level " + lvl;
                flag2 = false;

                //SceneManager.UnloadSceneAsync("gamescene");
                // SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);

            }

            if (GameObject.FindGameObjectWithTag("tank1") == null && score2 < 3 && flag1)
            {
                lvl += 1;
                score2 += 1;
                scoreText2.text = ":" + score2;

                lvlText.text = "Level " + lvl;
                flag1 = false;

                // SceneManager.UnloadSceneAsync("gamescene");
                // SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);

            }

        }


        else
        {
            scoreText1.text = ":" + score11;
            scoreText2.text = ":" + score22;

            lvlText.text = "Level " + lvll;
        }

    } 

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score1);
            stream.SendNext(score2);
            stream.SendNext(lvl);
        }

        else if (stream.IsReading)
        {
            score11 = (int)stream.ReceiveNext();
            score22 = (int)stream.ReceiveNext();
            lvll    = (int)stream.ReceiveNext();


        }
    }
}

/*        
       if (SceneManager.GetActiveScene().name != "gamescene_online")
           {
               SceneManager.SetActiveScene(SceneManager.GetSceneByName("gamescene_online"));
           } 


       if (!GameObject.FindGameObjectWithTag("tank2") && score1 < 3)
         {
               lvl += 1;
               score1 += 1;
               scoreText1.text = ":" + score1;

               lvlText.text = "Level " + lvl;
               //SceneManager.UnloadSceneAsync("gamescene");
              // SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);

         }

       if (!GameObject.FindGameObjectWithTag("tank1")&& score2<3)
       {
           lvl += 1;
           score2 += 1;
           scoreText2.text = ":" + score2;

               lvlText.text = "Level " + lvl;
              // SceneManager.UnloadSceneAsync("gamescene");
              // SceneManager.LoadScene("gamescene", LoadSceneMode.Additive);

       }

       if (score2 >=3 && !GameObject.FindGameObjectWithTag("winner"))
       {
           lvl -=1 ;
           lvlText.text = "Level " + lvl;

           winText.text = "Player 2 won " ;
           //Instantiate(WIN,transform);
       }
       if (score1 >= 3 && !GameObject.FindGameObjectWithTag("winner"))
       {
           lvl -= 1;
           lvlText.text = "Level " + lvl;

           winText.text = "Player 1 won " ;
          // Instantiate(WIN, transform);

       }*/