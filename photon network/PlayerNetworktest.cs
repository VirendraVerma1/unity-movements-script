using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworktest : MonoBehaviour {

	[SerializeField]private GameObject playerCamera;
	[SerializeField]private MonoBehaviour[] playerControllerScript;
	private PhotonView photonView;
	public GameObject Charecter;
	public int playerHealth=100;
	private void Start()
	{
		photonView = GetComponent<PhotonView> ();
		Initialize ();
	}

	private void Initialize()
	{
		if (photonView.isMine) {
			Charecter.SetActive (false);
		} else {

			playerCamera.SetActive (false);
			foreach (MonoBehaviour m in playerControllerScript) {
				m.enabled = false;
			}
		}
	}



	[PunRPC]
	public void ApplyDamage(int damage)
	{
		playerHealth -= damage;
		if (playerHealth <= 0) {
			Destroy (gameObject);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (playerHealth);

		} else  if(stream.isReading){
			playerHealth = (int)stream.ReceiveNext ();
		}
	}
}
