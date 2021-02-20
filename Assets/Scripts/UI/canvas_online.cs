using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class Canvas_online : MonoBehaviour//Pun, IPunObservable
{
    [SerializeField]
    public Text scoreText0;

    [SerializeField]
    public Text scoreText1;


    public int score0=0, score1=0;
    // private ExitGames.Client.Photon.Hashtable customscore = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        // customscore["player_score"] = score;

        scoreText0.text = " " + score0;
        scoreText1.text = " " + score1;

       // PhotonNetwork.LocalPlayer.CustomProperties = customscore;
      //  score = (int)PhotonNetwork.LocalPlayer.CustomProperties["player_score"];

    }

    private void Update()
    {
     

        if (PhotonNetwork.PlayerList[0].CustomProperties.ContainsKey("player_score1"))
        {
            score1 = (int)PhotonNetwork.PlayerList[0].CustomProperties["player_score1"];
        }



        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (PhotonNetwork.PlayerList[1].CustomProperties.ContainsKey("player_score0"))
            {
                score0 = (int)PhotonNetwork.PlayerList[1].CustomProperties["player_score0"];
            }

        }

        scoreText1.text = " " + score1;
        scoreText0.text = " " + score0;


    }

  

} 