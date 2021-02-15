using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class Manager_tank : MonoBehaviour 
{


    public GameObject playerPrefab;
    public GameObject playerPrefab2;
   // canvas_online c;
    //PhotonView PV1, PV2;
    // int ID1, ID2;






    void Start()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {
            if (!GameObject.FindGameObjectWithTag("tank1")&& !GameObject.FindGameObjectWithTag("tank2"))
            {

                PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
               // ID1 = GetComponent<PhotonView>().ViewID;

                Debug.Log("Setting master to blue tank");
               // Debug.Log(ID1);




            }

        }

        if (PhotonNetwork.IsMasterClient == false)
        {
            if (!GameObject.FindGameObjectWithTag("tank2"))
            {

                PhotonNetwork.Instantiate(playerPrefab2.name, playerPrefab2.transform.position, playerPrefab2.transform.rotation);

               // ID2 = GetComponent<PhotonView>().ViewID;

                Debug.Log("Setting NONmaster to red tank");
               // Debug.Log(ID2);


            }
        }

       
    }


    private void Update()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            if (GameObject.FindGameObjectWithTag("tank1")==null && GameObject.FindGameObjectWithTag("tank2")!=null)
            {

                Debug.Log("Setting master to blue tank");
                PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
                // ID2 = GetComponent<PhotonView>().ViewID;
                


            }


        }

        if (PhotonNetwork.IsMasterClient == false)
        {
            if (GameObject.FindGameObjectWithTag("tank2")==null)
            {

                Debug.Log("Setting NONmaster to red tank");
                PhotonNetwork.Instantiate(playerPrefab2.name, playerPrefab2.transform.position, playerPrefab2.transform.rotation);
                //  ID1 = GetComponent<PhotonView>().ViewID;
           
            }
        }
    }




}
/*  if (m_PlayerNumber==2  && photonView.IsMine) 
      {
          c.score1 += 1;
          c.lvl += 1;
      }
      if (m_PlayerNumber == 1 && photonView.IsMine)
      {
          c.score2 += 1;
          c.lvl += 1;

      }*/