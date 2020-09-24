using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {

	[SerializeField]
	private GameObject _roomListingPrefab;
	private GameObject RoomListingPrefab
	{
		get{
			return _roomListingPrefab;
		}
	}

	private List<RoomListing> _roomListingButtons = new List<RoomListing> ();
	private List<RoomListing> RoomListingBottons
	{
		get{
			return _roomListingButtons;
		}
	}

	private void OnReceivedRoomListUpdate()
	{
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();

		foreach (RoomInfo room in rooms) {
			RoomReceived (room);
		}

		RemoveOldRooms ();
	}

	private void RoomReceived(RoomInfo room)
	{
		int index = RoomListingBottons.FindIndex (x => x.RoomName == room.Name);

		if (index == -1) {
			if (room.IsVisible && room.PlayerCount < room.MaxPlayers) {
				GameObject roomListingObj = Instantiate (RoomListingPrefab);
				roomListingObj.transform.SetParent (transform, false);

				RoomListing roomListing = roomListingObj.GetComponent<RoomListing> ();
				RoomListingBottons.Add (roomListing);

				index = (RoomListingBottons.Count - 1);
			}
		}
		if (index != -1) {
			RoomListing roomListing = RoomListingBottons [index];
			roomListing.SetRoomNameText (room.Name);
			roomListing.Updated = true;
		}
	}

	private void RemoveOldRooms()
	{
		List<RoomListing> removeRooms = new List<RoomListing> ();

		foreach (RoomListing roomListing in RoomListingBottons) {
			if (!roomListing.Updated)
				removeRooms.Add (roomListing);
			else
				roomListing.Updated = false;
		}

		foreach (RoomListing roomListting in removeRooms) {
			GameObject roomListingObj = roomListting.gameObject;
			RoomListingBottons.Remove (roomListting);
			Destroy (roomListingObj); 
		}
	}
}
