using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class Launcher_tank : MonoBehaviourPunCallbacks {

    public GameObject connectedScreen;
    public GameObject connectingScreen;

    public GameObject disconnectedScreen;

    public InputField createRoomTF;
    public InputField joinRoomTF;


    public void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
        connectingScreen.SetActive(true);
    }


    public override void OnConnectedToMaster()

    {
        Debug.Log("player has connected to photon");

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if(disconnectedScreen.activeSelf)
            disconnectedScreen.SetActive(false);

        connectingScreen.SetActive(false);

        connectedScreen.SetActive(true);
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }
    public void OnClick_CreateRoom()
    {
        RoomOptions Options = new RoomOptions();
        Options.BroadcastPropsChangeToAll = true;
        Options.PublishUserId = true;
        Options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createRoomTF.text, Options, null);

    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Success");
        PhotonNetwork.LoadLevel(1);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("RoomFailed " + returnCode + " Message " + message);

    }

}
