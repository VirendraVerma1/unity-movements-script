using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMainCanvasManager : MonoBehaviour {

	public static RoomMainCanvasManager Instance;

	[SerializeField]
	private LobbyCanvas _lobbyCanvas;
	public LobbyCanvas LobbyCanvas
	{
		get{
			return _lobbyCanvas;
		}
	}


	// Use this for initialization
	void Awake () {
		Instance=this;
	}
	

}
