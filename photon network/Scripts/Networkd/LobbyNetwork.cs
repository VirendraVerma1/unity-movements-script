using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyNetwork : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("Connecting to network..");
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	private void OnConnectedToMaster()
	{
		print ("Connected to master..");
		PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

		PhotonNetwork.JoinLobby (TypedLobby.Default);
	}

	private void OnJoinedLobby()
	{
		print ("Joined Lobby");
	}
}
