using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class SwitchToCar : MonoBehaviour {

	public LayerMask layerToDetect;
	public Transform rayTranformPivot;
	//public string buttonPickup;

	private Transform itemAvailableForPickup;
	private RaycastHit hit;
	private float detectRange = 5;
	private float detectRadius = 1f;
	private bool itemInRange;

	private float labaelWidth = 200;
	private float labelHeight = 50;

	public GameObject bMW;
	public GameObject playercarcamera;
	public GameObject player;
	public GameObject playerincardrive;
	public GameObject playerStartPos;
	public GameObject playerHidePos;

	private bool seated=false;
	private bool Eswitchdelay = true;

	public FirstPersonController fpc;

	//for third person
	public GameObject playerbotcamera;
	public GameObject playerbotmaincamera;
	public ThirdPersonCharacter tpc;
	public ThirdPersonUserControl tpuc;
	public bool startDoorAnim;


	// Use this for initialization
	void Start () {
		bMW.GetComponent<secondcarmovementscript> ().enabled = false;
		playerincardrive.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		CastRayForDetectingItems ();
		CheckForItemPickupAttempt ();
	}

	void CastRayForDetectingItems()
	{
		if (Physics.SphereCast (rayTranformPivot.position, detectRadius, rayTranformPivot.forward, out hit, detectRange, layerToDetect)) {
			itemAvailableForPickup = hit.transform;
			itemInRange = true;


		} else {
			itemInRange = false;

		}

	}

	void CheckForItemPickupAttempt ()
	{

		if (Input.GetKey(KeyCode.E) && Time.timeScale > 0 && itemInRange==true&&seated==false) {
			
			//Eswitchdelay = false;
			seated = true;
			//itemInRange = false;
			startDoorAnim=true;
			playerincardrive.SetActive (true);
			bMW.GetComponent<secondcarmovementscript> ().enabled = true;
			player.SetActive (true);
			playercarcamera.SetActive (true);
			Invoke("wait",.1f);

			//for third person
			playerbotcamera.SetActive(false);
			playerbotmaincamera.SetActive(false);
			tpc.GetComponent<ThirdPersonCharacter> ().enabled = false;
			tpc.GetComponent<ThirdPersonUserControl> ().enabled = false;

			//for first person

			//fpc.GetComponent<FirstPersonController> ().enabled = false;
			//fpc.GetComponent<Camera> ().enabled = false;
			//fpc.GetComponent<AudioSource> ().enabled = false;

			player.transform.position=playerHidePos.transform.position;
		}
		else if (Input.GetKey(KeyCode.E) && Time.timeScale > 0 && seated==true) {


			startDoorAnim = false;
			bMW.GetComponent<secondcarmovementscript> ().enabled = false;
			playercarcamera.SetActive (false);
			player.SetActive (true);
			playerincardrive.SetActive (false);
			player.transform.position = playerStartPos.transform.position;

			seated = false;

			//for third person
			playerbotcamera.SetActive(true);
			playerbotmaincamera.SetActive(true);
			tpc.GetComponent<ThirdPersonCharacter> ().enabled = true;
			tpc.GetComponent<ThirdPersonUserControl> ().enabled = true;



			//for first person

			//fpc.GetComponent<FirstPersonController> ().enabled = true;
			//fpc.GetComponent<AudioSource> ().enabled = true;
			//fpc.GetComponent<Camera> ().enabled = true;
		} 
	}

	void wait()
	{
		seated = true;
		print (seated);
		print (Time.timeScale);


	}
}
