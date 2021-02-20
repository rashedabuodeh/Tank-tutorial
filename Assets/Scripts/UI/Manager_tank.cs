using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public  class Manager_tank : MonoBehaviour//Pun, IPunObservable
{
    // public List<GameObject> players = new List<GameObject>();
    public Transform[] Spawns;


    //public GameObject[] players ;
    public GameObject playerPrefab ;
    public GameObject playerPrefab2 ;
    public GameObject player1,player2 ;

    public bool repeat1, flag, repeat2;//, START=false;


    void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 15;

        if (PhotonNetwork.IsMasterClient == true)
        {
            player1=PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
            Debug.Log("Setting master to blue tank");
        }

        if (PhotonNetwork.IsMasterClient == false)
        {
            player2=PhotonNetwork.Instantiate(playerPrefab2.name, playerPrefab2.transform.position, playerPrefab2.transform.rotation);
            Debug.Log("Setting NONmaster to red tank");
        }      
    }

   private void Update()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            if (flag )
            {
                //PhotonNetwork.LocalPlayer.SetCustomProperties()
               player1.SetActive(false);
               player1.SetActive(true);
               player1.transform.position = Spawns[0].position;
               player1.transform.rotation = Spawns[0].rotation;
               flag = false;
            }

        }
        if (PhotonNetwork.IsMasterClient == false)
        {
            if (flag)
            {
                player2.SetActive(false);
                player2.SetActive(true);
                player2.transform.position = Spawns[1].position;
                player2.transform.rotation = Spawns[1].rotation;
                flag = false;
            }

        }
    }

    public void Rest(Transform T , int i)
    {
       T.position = Spawns[i].position;
       T.rotation = Spawns[i].rotation;

    }
}
