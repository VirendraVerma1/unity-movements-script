using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConectivity : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    int playerIndex = 1;
    public GameObject MainNetworkPrefab;
    public Transform[] SpawnPositions;

    public GameObject Canvas;
    void Start()
    {
        Canvas.SetActive(false);
        print("Connecting to server");
        //photonView = GetComponent<PhotonView>();
        //PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to photon server");
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRandomGameButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnected()
    {
        print("Connected to internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        CreateAndJoinRoom();
    }

    void CreateAndJoinRoom()
    {
        string randomRoomName = "R" + Random.Range(10000, 999999);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        print(newPlayer.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        playerIndex = PhotonNetwork.PlayerList.Length;
       
        
       UpdateTeams(playerIndex);

        Canvas.SetActive(false);
        print("joined to room" + PhotonNetwork.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);
        
    }

    
    public void UpdateTeams(int n)
    {
        Canvas.SetActive(false);
        print(playerIndex);
        GameObject go;
        go = PhotonNetwork.Instantiate(MainNetworkPrefab.name, SpawnPositions[playerIndex - 1].position, SpawnPositions[playerIndex - 1].rotation, 0);
        
    }

    
    

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        print("Disconnected from servers "+cause.ToString());
    }
}
