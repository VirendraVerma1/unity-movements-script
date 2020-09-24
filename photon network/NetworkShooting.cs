using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkShooting : MonoBehaviour {

	public Transform shootpoint;
	private int damage=25;
	[SerializeField]
	public GameObject Charecter;
	private Animator anim;

	private PhotonView pv;
	private void Start()
	{
		pv = GetComponent<PhotonView> ();
		anim = Charecter.GetComponent<Animator> ();
	}
	private void Update()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
			Fire ();
		}
		float translation = Input.GetAxis("Vertical");
		if (!pv.isMine) {
			if (translation > 0) {
				anim.SetBool ("isWalk", true);
				anim.SetBool ("isIdle", false);
			} else if (translation == 0) {
				anim.SetBool ("isIdle", true);
				anim.SetBool ("isWalk", false);
			}
		}
	}

	private void Fire()
	{
		RaycastHit hit;

		if (Physics.Raycast (shootpoint.position, shootpoint.forward, out hit)) {
			if (hit.transform.CompareTag ("Player")) {
				PhotonView pv = hit.transform.GetComponent<PhotonView> ();

				if (pv) {
					pv.RPC ("ApplyDamage", PhotonTargets.All, damage);
				}
			}
		}
	}
}
