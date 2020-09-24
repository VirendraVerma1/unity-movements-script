using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : MonoBehaviour {

	[SerializeField]
	private Text status;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject lobbycamera;
	[SerializeField]
	private Transform spawnPoint;
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	public virtual void OnJoinedLobby()
	{
		PhotonNetwork.JoinOrCreateRoom ("New", null, null);
		print ("joined lobby");
	}

	public virtual void OnJoinedRoom()
	{
		print ("joined room");
		PhotonNetwork.Instantiate (player.name, spawnPoint.position, spawnPoint.rotation, 0);
		lobbycamera.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		status.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}
}
